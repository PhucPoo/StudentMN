using StudentMN.DTOs.Request;
using StudentMN.DTOs.Response;

namespace StudentMN.Services.Interfaces
{
    public interface ICourseSectionService
    {
        Task<PagedResponse<CourseSectionResponseDTO>> GetAllCourseSection(int pageNumber, int pageSize, string? search);
        Task<CourseSectionResponseDTO> CreateCourseSection(CourseSectionRequestDTO courseSection);
        Task<CourseSectionResponseDTO> UpdateCourseSection(int id, CourseSectionRequestDTO courseSection);
        Task<bool> DeleteCourseSection(int id);
    }
}
