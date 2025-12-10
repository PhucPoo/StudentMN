using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudentMN.Data;
using StudentMN.DTOs.Request;
using StudentMN.DTOs.Response;
using StudentMN.Models.Class;

namespace StudentMN.Services
{
    public class ClassService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;
        public ClassService(AppDbContext context, IMapper mapper, IAuthService authService)
        {
            _context = context;
            _mapper = mapper;
            _authService = authService;
        }
        // Xem danh sách lớp
        public async Task<PagedResponse<ClassesResponseDTO>> GetAllAsync(int pageNumber = 1, int pageSize = 8, string? search = null)
        {
            var query = _context.Classes.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(s => s.ClassName.Contains(search));
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
        public async Task<ClassesResponseDTO> CreateAsync(ClassesRequestDTO dto)
        {
            var Class = _mapper.Map<Classes>(dto);

            _context.Classes.Add(Class);
            await _context.SaveChangesAsync();
            return _mapper.Map<ClassesResponseDTO>(Class);
        }

        //Cập nhật lớp mới
        public async Task<ClassesResponseDTO> UpdateAsync(int id, ClassesRequestDTO dto)
        {
            var Class = await _context.Classes.FindAsync(id);
            if (Class == null) return null;

            _mapper.Map(dto, Class);
            await _context.SaveChangesAsync();
            return _mapper.Map<ClassesResponseDTO>(Class);
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var Class = await _context.Classes.FindAsync(id);
            if (Class == null) return false;
            Class.IsDelete = true;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
