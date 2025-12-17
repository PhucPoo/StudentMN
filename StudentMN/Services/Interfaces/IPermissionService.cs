using StudentMN.DTOs.Request;
using StudentMN.DTOs.Response;
using StudentMN.Models.Entities.PermissionModels;

namespace StudentMN.Services.Interfaces

{
    public interface IPermissionService
    {
        Task<PagedResponse<PermissionDTO>> GetAllPermissionAsync(int pageNumber,int pageSize,string? search);
        Task<Permission> GetPermissionByIdAsync(int id);
        Task<PermissionDTO> CreatePermissionAsync(PermissionRequestDTO permission);
        Task<Permission> UpdatePermissionAsync(int id, Permission permission);
        Task<bool> DeletePermissionAsync(int id);
    }
}
