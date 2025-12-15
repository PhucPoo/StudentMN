using Microsoft.EntityFrameworkCore;
using StudentMN.Data;
using StudentMN.DTOs.Request;
using StudentMN.DTOs.Response;
using StudentMN.Models.PermissionModels;

namespace StudentMN.Services.PermissionService
{
    public class RolePermissionService : IRolePermissionService
    {
        private readonly AppDbContext _context;
        public RolePermissionService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<RolePermissionDTO>> GetRolesWithPermissionsAsync()
        {
            var roles = await _context.Roles
                .Where(r => !r.IsDelete)
                .ToListAsync();

            var permissions = await _context.Permissions
                .Where(p => !p.IsDelete)
                .ToListAsync();

            var rolePermissions = await _context.RolePermissions
                .Where(rp => !rp.IsDelete)
                .ToListAsync();

            var result = roles.Select(role => new RolePermissionDTO
            {
                RoleId = role.Id,
                RoleName = role.RoleName,
                Permissions = permissions.Select(p => new PermissionDTO
                {
                    Id = p.Id,
                    PermissionName = p.PermissionName,
                    Description = p.Description,
                    IsAssigned = rolePermissions.Any(rp => rp.RoleId == role.Id && rp.PermissionId == p.Id)
                }).ToList()
            }).ToList();

            return result;
        }
        public async Task<bool> UpdateRolePermissionsAsync(RolePermissionRequestDTO request)
        {
            var existingRPs = _context.RolePermissions
                .Where(rp => rp.RoleId == request.RoleId);
            _context.RolePermissions.RemoveRange(existingRPs);

            var newRPs = request.PermissionIds.Select(pid => new RolePermission
            {
                    RoleId = request.RoleId,
                    PermissionId = pid,
                    CreatedAt = DateTime.Now,
                    IsDelete = false
                });

            await _context.RolePermissions.AddRangeAsync(newRPs);
            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<bool> RemovePermissionFromRoleAsync(int roleId, int permissionId)
        {
            var rolePermission = await _context.RolePermissions
                .FirstOrDefaultAsync(rp =>
                    rp.RoleId == roleId &&
                    rp.PermissionId == permissionId &&
                    !rp.IsDelete);

            if (rolePermission == null)
                return false;

            _context.RolePermissions.Remove(rolePermission);

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
