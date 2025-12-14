using StudentMN.DTOs.Request;
using StudentMN.DTOs.Response;
using StudentMN.Models.Account;

namespace StudentMN.Services.AuthService
{
    public interface IAuthService
    {
        string HashPassword(string password);
        bool VerifyPassword(string password, string hash);
        Task<LoginResponse> LoginAsync(LoginRequest request);
        string GenerateJwtToken(User user, string roleName);
     

        string GenerateRefreshToken();
        Task SaveRefreshTokenAsync(int userId, string refreshToken);
        Task<bool> ValidateRefreshTokenAsync(int userId, string refreshToken);

        Task<LoginResponse> RefreshTokenAsync(string refreshToken);
        Task<bool> ValidateTokenAsync(string token);
        Task<User?> GetCurrentUserAsync(int userId);
    }
}
