using StudentMN.DTOs.Request;
using StudentMN.DTOs.Response;

namespace StudentMN.Services.Interfaces
{
    public interface IMajorService
    {
        Task<PagedResponse<MajorResponseDTO>> GetAllMajor(int pageNumber = 1, int pageSize = 8, string? search = null);
        Task<MajorResponseDTO?> GetMajorById(int id);

        Task<MajorResponseDTO> CreateMajor(MajorRequestDTO dto);

        Task<MajorResponseDTO?> UpdateMajor(int id, MajorRequestDTO dto);

        Task<bool> DeleteMajor(int id);
    }
}
