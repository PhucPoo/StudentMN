using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentMN.DTOs;
using StudentMN.DTOs.Response;
using StudentMN.Models;
using StudentMN.Services;
using System.Security.Claims;

namespace StudentMN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new LoginResponse
                {
                    Success = false,
                    Message = "Dữ liệu không hợp lệ"
                });
            }

            var result = await _authService.LoginAsync(request);

            if (!result.Success)
            {
                return Unauthorized(result);
            }

            return Ok(result);
        }

       
        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                return Unauthorized(new
                {
                    success = false,
                    message = "Token không hợp lệ"
                });
            }

            var user = await _authService.GetCurrentUserAsync(userId);

            if (user == null)
            {
                return NotFound(new
                {
                    success = false,
                    message = "Không tìm thấy người dùng"
                });
            }

            return Ok(new
            {
                success = true,
                data = new
                {
                    id = user.Id,
                    username = user.Username,
                    fullName = user.FullName,
                    email = user.Email,
                    role = user.Role.RoleName,
                    createdAt = user.CreatedAt
                }
            });
        }

        

        [HttpPost("validate-token")]
        public async Task<IActionResult> ValidateToken([FromBody] string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Token không được để trống"
                });
            }

            var isValid = await _authService.ValidateTokenAsync(token);

            return Ok(new
            {
                success = true,
                isValid = isValid
            });
        }

        [Authorize]
        [HttpPost("logout")]
        public IActionResult Logout()
        {
           
            return Ok(new
            {
                success = true,
                message = "Đăng xuất thành công"
            });
        }
    }
}