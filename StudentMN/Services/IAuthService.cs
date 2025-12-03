using StudentMN.DTOs.Response;
using StudentMN.Models;

namespace StudentMN.Services
{
    public interface IAuthService
    {
        string HashPassword(string password);
        bool VerifyPassword(string password, string hash);
        Task<LoginResponse> LoginAsync(LoginRequest request);
        string GenerateJwtToken(User user, string roleName);
        Task<bool> ValidateTokenAsync(string token);
        Task<User> GetCurrentUserAsync(int userId);
    }
}
