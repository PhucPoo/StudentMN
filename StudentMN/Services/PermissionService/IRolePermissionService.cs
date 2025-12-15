using StudentMN.DTOs.Request;
using StudentMN.DTOs.Response;

namespace StudentMN.Services.PermissionService
{
    public interface IRolePermissionService
    {
        Task<List<RolePermissionDTO>> GetRolesWithPermissionsAsync();
        Task<bool> UpdateRolePermissionsAsync(RolePermissionRequestDTO request);
        Task<bool> RemovePermissionFromRoleAsync(int roleId, int permissionId);
    }
}
