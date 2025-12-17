using StudentMN.DTOs.Request;
using StudentMN.DTOs.Response;
using StudentMN.Models.Class;
using StudentMN.Models.PermissionModels;

namespace StudentMN.Services.CourseSectionService
{
    public interface ICourseSectionService
    {
        Task<PagedResponse<CourseSectionResponseDTO>> GetAllCourseSectionAsync(int pageNumber, int pageSize, string? search);
        Task<CourseSection> GetCourseSectionByIdAsync(int id);
        Task<CourseSectionResponseDTO> CreateCourseSectionAsync(CourseSectionRequestDTO courseSection);
        Task<CourseSection> UpdateCourseSectionAsync(int id, CourseSection courseSection);
        Task<bool> DeleteCourseSectionAsync(int id);
    }
}
