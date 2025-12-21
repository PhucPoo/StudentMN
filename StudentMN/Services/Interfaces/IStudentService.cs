using StudentMN.DTOs.Request;
using StudentMN.DTOs.Response;

namespace StudentMN.Services.Interfaces
{
    public interface IStudentService
    {
        Task<PagedResponse<StudentResponseDTO>> GetAllStudent(int pageNumber = 1, int pageSize = 8, string? search = null);
        Task<StudentResponseDTO?> GetStudentById(int id);

        Task<StudentResponseDTO> CreateStudent(StudentRequestDTO dto);

        Task<StudentResponseDTO?> UpdateStudent(int id, StudentRequestDTO dto);

        Task<bool> DeleteStudent(int id);
    }
}
