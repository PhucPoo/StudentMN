using StudentMN.DTOs.Request;
using StudentMN.DTOs.Response;

namespace StudentMN.Services.Interfaces
{
    public interface ISubjectService
    {
        Task<PagedResponse<SubjectResponseDTO>> GetAllSubject(int pageNumber = 1, int pageSize = 8, string? search = null);
        Task<SubjectResponseDTO?> GetSubjectById(int id);

        Task<SubjectResponseDTO> CreateSubject(SubjectRequestDTO dto);

        Task<SubjectResponseDTO?> UpdateSubject(int id, SubjectRequestDTO dto);

        Task<bool> DeleteSubject(int id);
    }
}
