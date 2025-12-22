using StudentMN.DTOs.Request;
using StudentMN.DTOs.Response;

namespace StudentMN.Services.Interfaces
{
    public interface IEnrollmentCourseSection
    {
        Task<PagedResponse<EnrollmentResponseDTO>> GetAllEnrollments(int pageNumber, int pageSize, string? search);
        Task<EnrollmentResponseDTO> CreateEnrollment(EnrollmentRequestDTO enrollment);
        Task<EnrollmentResponseDTO> UpdateEnrollment(int id, EnrollmentRequestDTO enrollment);
        Task<bool> DeleteEnrollment(int id);

    }
}
