

using StudentMN.DTOs.Request;
using StudentMN.DTOs.Response;

namespace StudentMN.Services.Interfaces
{
    public interface ITeacherService
    {
        Task<PagedResponse<TeacherResponseDTO>> GetAllTeacher(int pageNumber = 1, int pageSize = 8, string? search = null);
        Task<TeacherResponseDTO?> GetTeacherById(int id);

        Task<TeacherResponseDTO> CreateTeacher(TeacherRequestDTO dto);

        Task<TeacherResponseDTO?> UpdateTeacher(int id, TeacherRequestDTO dto);

        Task<bool> DeleteTeacher(int id);
    }
}
