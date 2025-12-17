using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudentMN.Data;
using StudentMN.DTOs.Request;
using StudentMN.DTOs.Response;
using StudentMN.Models.Entities.Class;
using StudentMN.Models.Entities.PermissionModels;
using StudentMN.Services.Interfaces;

namespace StudentMN.Services
{
    public class ClassService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<ClassService> _logger;
        public ClassService(AppDbContext context, IMapper mapper, IAuthService authService,ILogger<ClassService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }
        // Xem danh sách lớp
        public async Task<PagedResponse<ClassesResponseDTO>> GetAllClassAsync(int pageNumber = 1, int pageSize = 8, string? search = null)
        {
            var query = _context.Classes.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(s => s.ClassName!=null && s.ClassName.Contains(search));
            }

            var totalCount = await query.CountAsync();

            var Classes = await query
                .OrderBy(s => s.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var ClasssDto = _mapper.Map<List<ClassesResponseDTO>>(Classes);

            return new PagedResponse<ClassesResponseDTO>
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize),
                Data = ClasssDto
            };
        }
        //Thêm lớp mới
        public async Task<ClassesResponseDTO> CreateClassAsync(ClassesRequestDTO dto)
        {
            var teacherExists = await _context.Teachers
                   .AnyAsync(t => t.Id == dto.TeacherId);

            if (!teacherExists)
            {
                throw new Exception("The teacher does not exist");
            }
            var Class = _mapper.Map<Classes>(dto);

            _context.Classes.Add(Class);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("Name err: {ExceptionName}, Details: {Message}", ex.GetType().Name, ex.Message);

            }
            return _mapper.Map<ClassesResponseDTO>(Class);
        }

        //Cập nhật lớp mới
        public async Task<ClassesResponseDTO?> UpdateClassAsync(int id, ClassesRequestDTO dto)
        {
            var Class = await _context.Classes.FindAsync(id);
            if (Class == null) return null;

            _mapper.Map(dto, Class);
            await _context.SaveChangesAsync();
            return _mapper.Map<ClassesResponseDTO>(Class);
        }
        public async Task<bool> DeleteClassAsync(int id)
        {
            var Class = await _context.Classes.FindAsync(id);
            if (Class == null) return false;
            _context.Classes.Remove(Class);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
