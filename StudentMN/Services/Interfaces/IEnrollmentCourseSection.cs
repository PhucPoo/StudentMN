using StudentMN.DTOs.Request;
using StudentMN.DTOs.Response;

namespace StudentMN.Services.Interfaces
{
    public interface IEnrollmentCourseSection
    {
        Task<PagedResponse<EnrollmentResponseDTO>> GetAllEnrollmentsAsync(int pageNumber, int pageSize, string? search);
        Task<EnrollmentResponseDTO> CreateEnrollmentAsync(EnrollmentRequestDTO enrollment);
        Task<bool> DeleteEnrollmentAsync(int id);

    }
}
