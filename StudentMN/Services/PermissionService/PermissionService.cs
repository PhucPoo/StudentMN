using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudentMN.Data;
using StudentMN.DTOs.Request;
using StudentMN.DTOs.Response;
using StudentMN.Models.PermissionModels;

namespace StudentMN.Services.PermissionService
{
    public class PermissionService : IPermissionService
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        public PermissionService(AppDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PagedResponse<PermissionDTO>> GetAllPermissionAsync(int pageNumber = 1, int pageSize = 8, string? search = null)
        {
            var query = _context.Permissions.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(s => s.PermissionName != null && s.PermissionName.Contains(search));
            }

            var totalCount = await query.CountAsync();

            var permissions = await query
                .OrderBy(s => s.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var permissionsDto = _mapper.Map<List<PermissionDTO>>(permissions);

            return new PagedResponse<PermissionDTO>
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize),
                Data = permissionsDto
            };
        }

        public async Task<Permission> GetPermissionByIdAsync(int id)
        {
            return await _context.Permissions
                .FirstOrDefaultAsync(p => p.Id == id && !p.IsDelete);
        }

        public async Task<PermissionDTO> CreatePermissionAsync(
     PermissionRequestDTO request)
        {
            var permission = new Permission
            {
                PermissionName = request.PermissionName,
                Description = request.Description,
                CreatedAt = DateTime.Now,
                IsDelete = false
            };

            _context.Permissions.Add(permission);
            await _context.SaveChangesAsync();

            return new PermissionDTO
            {
                Id = permission.Id,
                PermissionName = permission.PermissionName,
                Description = permission.Description
            };
        }


        public async Task<Permission> UpdatePermissionAsync(int id, Permission permission)
        {
            var existing = await _context.Permissions.FindAsync(id);
            if (existing == null || existing.IsDelete) return null;

            existing.PermissionName = permission.PermissionName;
            existing.Description = permission.Description;
            existing.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeletePermissionAsync(int id)
        {
            var existing = await _context.Permissions.FindAsync(id);
            if (existing == null || existing.IsDelete) return false;

            existing.IsDelete = true;
            existing.UpdatedAt = DateTime.Now;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
