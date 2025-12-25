using StudentMN.Models.Entities.Class;

namespace StudentMN.Repositories.Interface
{
    public interface IEnrollmentRepository
    {
        Task<List<EnrollmentCourseSection>> GetAllEnrollmentAsync();
        Task<List<EnrollmentCourseSection>> GetEnrollmentsByStudentIdAsync(int studentId);
        Task<EnrollmentCourseSection> AddEnrollmentAsync(EnrollmentCourseSection EnrollmentEntity);
        Task<EnrollmentCourseSection> GetEnrollmentsByIdAsync(int Id);
        Task UpdateEnrollmentAsync(EnrollmentCourseSection EnrollmentEntity);
        Task DeleteEnrollmentAsync(EnrollmentCourseSection EnrollmentEntity);
        Task<bool> ExistsAsync(int id);
    }
}
