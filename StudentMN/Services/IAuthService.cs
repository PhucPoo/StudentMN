
using StudentMN.DTOs;
using StudentMN.Models;

namespace StudentMN.Services
{
    public interface IAuthService
    {
        Task<LoginResponse> LoginAsync(LoginRequest request);
        string GenerateJwtToken(User user, string roleName);
        Task<bool> ValidateTokenAsync(string token);
        Task<User> GetCurrentUserAsync(int userId);
    }
}
