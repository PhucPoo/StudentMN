using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using StudentMN.Data;
using StudentMN.DTOs.Request;
using StudentMN.DTOs.Response;
using StudentMN.Models.Account;
using StudentMN.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace StudentMN.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _context;


        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerifyPassword(string password, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }

        public AuthService(IUserRepository userRepository, IConfiguration configuration, AppDbContext context)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _context = context;
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.Username) ||
                    string.IsNullOrWhiteSpace(request.Password))
                {
                    return new LoginResponse
                    {
                        Success = false,
                        Message = "Tên đăng nhập và mật khẩu không được để trống"
                    };
                }

                var user = await _userRepository.GetUserByUsernameAsync(request.Username);
                if (user == null)
                {
                    return new LoginResponse
                    {
                        Success = false,
                        Message = "Tên đăng nhập không tồn tại"
                    };
                }

                bool isValidPassword = await _userRepository.ValidateCredentialsAsync(
                    request.Username, request.Password);

                if (!isValidPassword)
                {
                    return new LoginResponse
                    {
                        Success = false,
                        Message = "Mật khẩu của bạn không đúng"
                    };
                }
                var tokens = await GenerateTokens(user);

                return new LoginResponse
                {
                    Success = true,
                    Message = "Đăng nhập thành công",
                    AccessToken = tokens.accessToken,
                    RefreshToken = tokens.refreshToken,
                    ExpiredAt = tokens.expires,
                    User = new UserInfo
                    {
                        Id = user.Id,
                        Username = user.Username,
                        FullName = user.FullName,
                        Email = user.Email,
                        Role = user.Role?.RoleName
                    }
                };
            }
            catch (Exception ex)
            {
                return new LoginResponse
                {
                    Success = false,
                    Message = $"Lỗi hệ thống: {ex.Message}"
                };
            }
        }


        public string GenerateJwtToken(User user, string roleName)
        {
            var jwtKey = _configuration["Jwt:Key"]
                ?? throw new InvalidOperationException("Jwt:Key bị thiếu trong cấu hình");

            var securityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtKey)
            );
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            if (string.IsNullOrWhiteSpace(user.Username))
            {
                throw new Exception("Username không hợp lệ");
            }
            if (string.IsNullOrWhiteSpace(user.Email))
            {
                throw new Exception("Email không hợp lệ");
            }
            if (string.IsNullOrWhiteSpace(user.FullName))
            {
                throw new Exception("FullName không hợp lệ");
            }
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email ),
                new Claim(ClaimTypes.Role, roleName),
                new Claim("FullName", user.FullName )
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(15),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }



        public async Task<bool> ValidateTokenAsync(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]
                        ?? throw new InvalidOperationException("Jwt:Key bị thiếu trong cấu hình")
                );

                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = _configuration["Jwt:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = _configuration["Jwt:Audience"],
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                return true;
            }
            catch
            {
                return false;
            }
        }
        public string? GetRoleFromToken(string token)
        {
            if (string.IsNullOrEmpty(token)) return "User";

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);


            var roleClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
            return roleClaim?.Value;
        }
        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        private async Task<(string accessToken, string refreshToken, DateTime expires)> GenerateTokens(User user)
        {
            if (user.Role == null && user.RoleId != 0)
            {
                user.Role = await _context.Roles
                    .FirstOrDefaultAsync(r => r.Id == user.RoleId)
                    ?? throw new InvalidOperationException($"Không tìm thấy vai trò có Id là = {user.RoleId}");
            }
            string? roleName = user.Role?.RoleName;
            if (string.IsNullOrEmpty(roleName))
                throw new Exception("User không có role hợp lệ");

            string accessToken = GenerateJwtToken(user, roleName);

            string refreshToken = GenerateRefreshToken();
            DateTime refreshExpiry = DateTime.UtcNow.AddDays(7);

            // Lưu refresh token vào DB
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = refreshExpiry;

            await _userRepository.UpdateAsync(user);
            //await _userRepository.SaveAsync(); 

            return (accessToken, refreshToken, refreshExpiry);
        }
        public async Task<LoginResponse> RefreshTokenAsync(string refreshToken)
        {
            var user = await _userRepository.GetUserByRefreshTokenAsync(refreshToken);

            if (user == null || user.RefreshToken != refreshToken)
                return new LoginResponse { Success = false, Message = "Refresh token không hợp lệ" };

            if (user.RefreshTokenExpiryTime < DateTime.UtcNow)
                return new LoginResponse { Success = false, Message = "Refresh token đã hết hạn" };

            var tokens = await GenerateTokens(user);
            var role = _context.Roles.FirstOrDefault(r => r.Id == user.RoleId);
            var roleName = role?.RoleName ?? "NoRole";

            return new LoginResponse
            {
                Success = true,
                Message = "Làm mới token thành công",
                AccessToken = tokens.accessToken,
                RefreshToken = tokens.refreshToken,
                ExpiredAt = tokens.expires,
                User = new UserInfo
                {
                    Id = user.Id,
                    Username = user.Username,
                    FullName = user.FullName,
                    Email = user.Email,
                    Role = roleName,
                }
            };
        }
        public async Task SaveRefreshTokenAsync(int userId, string refreshToken)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null) return;

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

            await _userRepository.UpdateAsync(user);
        }
        public async Task<bool> ValidateRefreshTokenAsync(int userId, string refreshToken)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null) return false;

            return user.RefreshToken == refreshToken &&
                   user.RefreshTokenExpiryTime > DateTime.UtcNow;
        }



        public async Task<User?> GetCurrentUserAsync(int userId)
        {
            return await _userRepository.GetUserByIdAsync(userId);
        }
    }
}