using StudentMN.DTOs.Request;
using StudentMN.DTOs.Response;

namespace StudentMN.Services.Interfaces
{
    public interface ICourseSectionService
    {
        Task<PagedResponse<CourseSectionResponseDTO>> GetAllCourseSectionAsync(int pageNumber, int pageSize, string? search);
        Task<CourseSectionResponseDTO> CreateCourseSectionAsync(CourseSectionRequestDTO courseSection);
        Task<CourseSectionResponseDTO> UpdateCourseSectionAsync(int id, CourseSectionRequestDTO courseSection);
        Task<bool> DeleteCourseSectionAsync(int id);
    }
}
