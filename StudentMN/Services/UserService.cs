using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudentMN.Data;
using StudentMN.DTOs.Request;
using StudentMN.DTOs.Response;
using StudentMN.Models;

namespace StudentMN.Services
{
    public class UserService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;

        public UserService(AppDbContext context, IMapper mapper , IAuthService authService)
        {
            _context = context;
            _mapper = mapper;
            _authService = authService;
        }

        // Xem danh sách người dùng
        public async Task<List<UserResponseDTO>> GetAllAsync()
        {
            
            var users = await _context.Users.ToListAsync();
            return _mapper.Map<List<UserResponseDTO>>(users);
        }

        // Thêm tài khoản mới
        public async Task<UserResponseDTO> CreateAsync(UserRequestDTO dto)
        {
            var user = _mapper.Map<User>(dto);
            user.Password = _authService.HashPassword(dto.Password);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return _mapper.Map<UserResponseDTO>(user);
        }

        // Cập nhật tài khoản
        public async Task<UserResponseDTO> UpdateAsync(int id, UserRequestDTO dto)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return null;

            _mapper.Map(dto, user);
            await _context.SaveChangesAsync();
            return _mapper.Map<UserResponseDTO>(user);
        }

        // Xóa tài khoản
        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
