using StudentMN.Models.Entities.Class;

namespace StudentMN.Repositories.Interface
{
    public interface IEnrollmentRepository
    {
        Task<List<EnrollmentCourseSection>> GetAllEnrollmentAsync();
        Task<EnrollmentCourseSection?> GetEnrollmentByIdAsync(int id);
        Task<EnrollmentCourseSection> AddEnrollmentAsync(EnrollmentCourseSection EnrollmentEntity);
        Task UpdateEnrollmentAsync(EnrollmentCourseSection EnrollmentEntity);
        Task DeleteEnrollmentAsync(EnrollmentCourseSection EnrollmentEntity);
        Task<bool> ExistsAsync(int id);
    }
}
