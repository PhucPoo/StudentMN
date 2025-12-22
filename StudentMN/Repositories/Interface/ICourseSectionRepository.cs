
using StudentMN.Models.Entities.Class;

namespace StudentMN.Repositories.Interface
{
    public interface ICourseSectionRepository
    {
        Task<List<CourseSection>> GetAllCourseSectionAsync();
        Task<CourseSection?> GetCourseSectionByIdAsync(int id);
        Task<CourseSection> AddCourseSectionAsync(CourseSection CourseSectionEntity);
        Task UpdateCourseSectionAsync(CourseSection CourseSectionEntity);
        Task DeleteCourseSectionAsync(CourseSection CourseSectionEntity);
        Task<bool> ExistsAsync(int id);
    }
}
