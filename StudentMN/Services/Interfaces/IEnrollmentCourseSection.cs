using StudentMN.DTOs.Request;
using StudentMN.DTOs.Response;
using StudentMN.Models.Entities.Class;

namespace StudentMN.Services.Interfaces
{
    public interface IEnrollmentCourseSection
    {
        Task<PagedResponse<EnrollmentResponseDTO>> GetAllEnrollments(int pageNumber, int pageSize, string? search);
        Task<EnrollmentResponseDTO> CreateEnrollment(EnrollmentRequestDTO enrollment);
        Task<List<EnrollmentResponseDTO>> GetEnrollmentsByStudentId(int studentId);
        Task<EnrollmentResponseDTO> GetEnrollmentsById(int Id);
        Task<bool> DeleteEnrollment(int studentId);

    }
}
