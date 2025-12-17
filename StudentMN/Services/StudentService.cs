using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudentMN.Data;
using StudentMN.DTOs.Request;
using StudentMN.DTOs.Response;
using StudentMN.Models.Entities.Account;
using StudentMN.Services.Interfaces;

namespace StudentMN.Services
{
    public class StudentService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public StudentService(AppDbContext context, IMapper mapper, IAuthService authService)
        {
            _context = context;
            _mapper = mapper;
        }

        // Xem danh sách sinh viên
        public async Task<PagedResponse<StudentResponseDTO>> GetAllStudentAsync(int pageNumber = 1,int pageSize = 8,string? search = null)
        {
            var query = _context.Students.Include(s => s.User)
                                         .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(s => s.StudentCode != null && s.StudentCode.Contains(search));
            }

            var totalCount = await query.CountAsync();

            var students = await query
                .OrderBy(s => s.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var studentsDto = _mapper.Map<List<StudentResponseDTO>>(students);

            return new PagedResponse<StudentResponseDTO>
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize),
                Data = studentsDto
            };
        }
        public async Task<StudentResponseDTO?> GetStudentByUserIdAsync(int userId)
        {
            var student = await _context.Students
                .Include(s => s.User) 
                .FirstOrDefaultAsync(s => s.UserId == userId);

            if (student == null) return null;

            var dto = _mapper.Map<StudentResponseDTO>(student);
            dto.FullName = student.User?.FullName;
            dto.Email = student.User?.Email;

            return dto;
        }


        // Thêm sinh viên mới
        public async Task<StudentResponseDTO> CreateStudentAsync(StudentRequestDTO dto)
        {
            var student = _mapper.Map<Student>(dto);

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == dto.UserId)
                ?? throw new InvalidOperationException($"Không tìm thấy người dùng có Id là = {dto.UserId}");

            student.UserId = user.Id;

            _context.Students.Add(student);
            await _context.SaveChangesAsync();
            var response= _mapper.Map<StudentResponseDTO>(student);
            response.FullName = user.FullName;
            response.Email = user.Email;
            return response;
        }

        // Cập nhật sinh viên
        public async Task<StudentResponseDTO?> UpdateStudentAsync(int id, StudentRequestDTO dto, string role)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null) return null;

            if (role != "Admin")
            {
                dto.StudentCode = student.StudentCode;
            }

            _mapper.Map(dto, student);
            await _context.SaveChangesAsync();
            return _mapper.Map<StudentResponseDTO>(student);
        }

        // Xóa tài khoản
        public async Task<bool> DeleteStudentAsync(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null) return false;
            student.IsDelete = true;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
