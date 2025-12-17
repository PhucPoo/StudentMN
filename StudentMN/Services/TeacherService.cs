using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudentMN.Data;
using StudentMN.DTOs.Request;
using StudentMN.DTOs.Response;
using StudentMN.Models.Entities.Account;
using StudentMN.Services.Interfaces;

namespace TeacherMN.Services
{
    public class TeacherService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public TeacherService(AppDbContext context, IMapper mapper, IAuthService authService)
        {
            _context = context;
            _mapper = mapper;
        }

        // Xem danh sách giảng viên
        public async Task<PagedResponse<TeacherResponseDTO>> GetAllTeacherAsync(int pageNumber = 1, int pageSize = 8, string? search = null)
        {
            var query = _context.Teachers.Include(s => s.User)
                                         .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(s => s.TeacherCode!=null && s.TeacherCode.Contains(search));
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
        public async Task<TeacherResponseDTO?> GetTeacherByUserIdAsync(int userId)
        {
            var teacher = await _context.Teachers
                .Include(s => s.User)
                .FirstOrDefaultAsync(s => s.UserId == userId);

            if (teacher == null) return null;

            var dto = _mapper.Map<TeacherResponseDTO>(teacher);
            dto.FullName = teacher.User?.FullName;
            dto.Email = teacher.User?.Email;

            return dto;
        }


        // Thêm giảng viên mới
        public async Task<TeacherResponseDTO> CreateTeacherAsync(TeacherRequestDTO dto)
        {
            var Teacher = _mapper.Map<Teacher>(dto);

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == dto.UserId);
            if (user == null)
            {
                throw new Exception("User không tồn tại");
            }

            Teacher.UserId = user.Id;

            _context.Teachers.Add(Teacher);
            await _context.SaveChangesAsync();
            var response = _mapper.Map<TeacherResponseDTO>(Teacher);
            response.FullName = user.FullName;
            response.Email = user.Email;
            return response;
        }

        // Cập nhật giảng viên
        public async Task<TeacherResponseDTO?> UpdateTeacherAsync(int id, TeacherRequestDTO dto)
        {
            var Teacher = await _context.Teachers.FindAsync(id);
            if (Teacher == null) return null;

            _mapper.Map(dto, Teacher);
            await _context.SaveChangesAsync();
            return _mapper.Map<TeacherResponseDTO>(Teacher);
        }

        // Xóa giảng viên
        public async Task<bool> DeleteTeacherAsync(int id)
        {
            var Teacher = await _context.Teachers.FindAsync(id);
            if (Teacher == null) return false;
            Teacher.IsDelete = true;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
