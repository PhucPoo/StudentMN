using StudentMN.DTOs.Request;
using StudentMN.DTOs.Response;

namespace StudentMN.Services.Interfaces
{
    public interface IUserService
    {
        Task<PagedResponse<UserResponseDTO>> GetAllUser(int pageNumber = 1, int pageSize = 8, string? search = null);

        Task<UserResponseDTO> CreateUser(UserRequestDTO dto);

        Task<UserResponseDTO?> UpdateUser(int id, UserRequestDTO dto);

        Task<bool> DeleteUser(int id);
    }
}
