using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudentMN.Data;
using StudentMN.DTOs.Request;
using StudentMN.DTOs.Response;
using StudentMN.Models.Account;
using StudentMN.Services;

namespace TeacherMN.Services
{
    public class TeacherService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;

        public TeacherService(AppDbContext context, IMapper mapper, IAuthService authService)
        {
            _context = context;
            _mapper = mapper;
            _authService = authService;
        }

        // Xem danh sách giảng viên
        public async Task<PagedResponse<TeacherResponseDTO>> GetAllAsync(int pageNumber = 1, int pageSize = 8, string? search = null)
        {
            var query = _context.Teachers.Include(s => s.User)
                                         .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(s => s.TeacherCode.Contains(search));
            }

            var totalCount = await query.CountAsync();

            var Teachers = await query
                .OrderBy(s => s.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var TeachersDto = _mapper.Map<List<TeacherResponseDTO>>(Teachers);

            return new PagedResponse<TeacherResponseDTO>
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize),
                Data = TeachersDto
            };
        }
        public async Task<TeacherResponseDTO> GetByUserIdAsync(int userId)
        {
            var teacher = await _context.Teachers
                .Include(s => s.User)
                .FirstOrDefaultAsync(s => s.UserId == userId);

            if (teacher == null) return null;

            var dto = _mapper.Map<TeacherResponseDTO>(teacher);
            dto.FullName = teacher.User.FullName;
            dto.Email = teacher.User.Email;

            return dto;
        }


        // Thêm giảng viên mới
        public async Task<TeacherResponseDTO> CreateAsync(TeacherRequestDTO dto)
        {
            var Teacher = _mapper.Map<Teacher>(dto);

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == dto.UserId);

            Teacher.UserId = user.Id;

            _context.Teachers.Add(Teacher);
            await _context.SaveChangesAsync();
            var response = _mapper.Map<TeacherResponseDTO>(Teacher);
            response.FullName = user.FullName;
            response.Email = user.Email;
            return response;
        }

        // Cập nhật giảng viên
        public async Task<TeacherResponseDTO> UpdateAsync(int id, TeacherRequestDTO dto)
        {
            var Teacher = await _context.Teachers.FindAsync(id);
            if (Teacher == null) return null;

            _mapper.Map(dto, Teacher);
            await _context.SaveChangesAsync();
            return _mapper.Map<TeacherResponseDTO>(Teacher);
        }

        // Xóa giảng viên
        public async Task<bool> DeleteAsync(int id)
        {
            var Teacher = await _context.Teachers.FindAsync(id);
            if (Teacher == null) return false;
            _context.Teachers.Remove(Teacher);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
