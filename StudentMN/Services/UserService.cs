using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentMN.Data;
using StudentMN.DTOs.Request;
using StudentMN.DTOs.Response;
using StudentMN.Models.Entities.Account;
using StudentMN.Services.Interfaces;

namespace StudentMN.Services
{
    public class UserService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;
        private readonly ILogger<UserService> _logger;

        public UserService(AppDbContext context, IMapper mapper, IAuthService authService, ILogger<UserService> logger)
        {
            _context = context;
            _mapper = mapper;
            _authService = authService;
            _logger = logger;
        }

        // Xem danh sách người dùng
        public async Task<PagedResponse<UserResponseDTO>> GetAllUserAsync(int pageNumber = 1, int pageSize = 8, string? search = null)
        {
            var query = _context.Users.Include(u => u.Role)
                                      .Where(u => u.IsActive)
                                      .AsQueryable();
            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(u => u.FullName != null && u.FullName.Contains(search));
            }
            var totalCount = await query.CountAsync();

            var users = await query
                .OrderBy(u => u.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var usersDto = _mapper.Map<List<UserResponseDTO>>(users);

            return new PagedResponse<UserResponseDTO>
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize),
                Data = usersDto
            };
        }

        // Thêm tài khoản mới
        public async Task<UserResponseDTO> CreateUserAsync(UserRequestDTO dto)
        {

            if (string.IsNullOrWhiteSpace(dto.Password))
            {
                _logger.LogWarning("Tao User that bai: Password rong | Username: {Username}", dto.Username);
                throw new ArgumentException("Password khong duoc de trong");
            }

            var usernameExists = await _context.Users.AnyAsync(u => u.Username == dto.Username && u.IsActive);
            if (usernameExists)
            {
                _logger.LogWarning("Tao user that bai: Username da ton tai | Username: {Username}", dto.Username);
                throw new ArgumentException("Username da ton tai");
            }

            var emailExists = await _context.Users.AnyAsync(u => u.Email == dto.Email && u.IsActive);
            if (emailExists)
            {
                _logger.LogWarning("tao user that bai: Email da ton tai | Email: {Email}", dto.Email);
                throw new ArgumentException("Email da ton tai");
            }

            var user = _mapper.Map<User>(dto);
            user.Password = _authService.HashPassword(dto.Password);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Tao user thanh cong | UserId: {UserId} | Username: {Username}", user.Id, user.Username);

            return _mapper.Map<UserResponseDTO>(user);
        }


        // Cập nhật tài khoản
        public async Task<UserResponseDTO?> UpdateUserAsync(int id, UserRequestDTO dto)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return null;

            _mapper.Map(dto, user);
            await _context.SaveChangesAsync();
            return _mapper.Map<UserResponseDTO>(user);
        }

        // Xóa tài khoản
        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;
            user.IsDelete = true;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
