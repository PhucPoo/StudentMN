using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentMN.DTOs;
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

        /// <summary>
        /// Đăng nhập sinh viên
        /// </summary>
        /// <param name="request">Thông tin đăng nhập</param>
        /// <returns>Token và thông tin user</returns>
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

        /// <summary>
        /// Lấy thông tin user hiện tại
        /// </summary>
        /// <returns>Thông tin user đang đăng nhập</returns>
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

        /// <summary>
        /// Test endpoint để kiểm tra authentication
        /// </summary>
        /// <returns>Thông tin từ token</returns>
        [Authorize]
        [HttpGet("test")]
        public IActionResult TestAuth()
        {
            var username = User.FindFirst(ClaimTypes.Name)?.Value;
            var role = User.FindFirst(ClaimTypes.Role)?.Value;
            var fullName = User.FindFirst("FullName")?.Value;

            return Ok(new
            {
                success = true,
                message = "Bạn đã xác thực thành công!",
                data = new
                {
                    username = username,
                    fullName = fullName,
                    role = role
                }
            });
        }

        /// <summary>
        /// Kiểm tra token có hợp lệ không
        /// </summary>
        /// <param name="token">JWT Token</param>
        /// <returns>True/False</returns>
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

        /// <summary>
        /// Đăng xuất (client side - chỉ xóa token)
        /// </summary>
        /// <returns>Thông báo</returns>
        [Authorize]
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            // JWT là stateless, không cần xử lý server-side
            // Client chỉ cần xóa token ở localStorage/sessionStorage
            return Ok(new
            {
                success = true,
                message = "Đăng xuất thành công"
            });
        }
    }
}