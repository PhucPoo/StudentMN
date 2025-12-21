


using StudentMN.DTOs.Request;
using StudentMN.DTOs.Response;

namespace ClassMN.Services.Interfaces
{
    public interface ICLassService
    {
        Task<PagedResponse<ClassesResponseDTO>> GetAllClass(int pageNumber = 1, int pageSize = 8, string? search = null);
        Task<ClassesResponseDTO?> GetClassById(int id);

        Task<ClassesResponseDTO> CreateClass(ClassesRequestDTO dto);

        Task<ClassesResponseDTO?> UpdateClass(int id, ClassesRequestDTO dto);

        Task<bool> DeleteClass(int id);
    }
}
