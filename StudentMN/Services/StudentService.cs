using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudentMN.Data;
using StudentMN.DTOs.Request;
using StudentMN.DTOs.Response;
using StudentMN.Models;

namespace StudentMN.Services
{
    public class StudentService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;

        public StudentService(AppDbContext context, IMapper mapper, IAuthService authService)
        {
            _context = context;
            _mapper = mapper;
            _authService = authService;
        }

        // Xem danh sách sinh viên
        public async Task<List<StudentResponseDTO>> GetAllAsync()
        {

            var students = await _context.Students.ToListAsync();
            return _mapper.Map<List<StudentResponseDTO>>(students);
        }

        // Thêm sinh viên mới
        public async Task<StudentResponseDTO> CreateAsync(StudentRequestDTO dto)
        {
            var student = _mapper.Map<Student>(dto);
            
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
            return _mapper.Map<StudentResponseDTO>(student);
        }

        // Cập nhật sinh viên
        public async Task<StudentResponseDTO> UpdateAsync(int id, StudentRequestDTO dto)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null) return null;

            _mapper.Map(dto, student);
            await _context.SaveChangesAsync();
            return _mapper.Map<StudentResponseDTO>(student);
        }

        // Xóa tài khoản
        public async Task<bool> DeleteAsync(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null) return false;
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
