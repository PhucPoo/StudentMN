using StudentMN.DTOs.Request;
using StudentMN.DTOs.Response;
using StudentMN.Models.Entities.Class;

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
