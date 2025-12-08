using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentMN.Data;
using StudentMN.DTOs.Request;
using StudentMN.DTOs.Response;
using StudentMN.Models.Account;

namespace StudentMN.Services
{
    public class UserService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;

        public UserService(AppDbContext context, IMapper mapper, IAuthService authService)
        {
            _context = context;
            _mapper = mapper;
            _authService = authService;
        }

        // Xem danh sách người dùng
        public async Task<PagedResponse<UserResponseDTO>> GetAllAsync(int pageNumber = 1, int pageSize = 8, string? search = null)
        {
            var query = _context.Users.Include(u => u.Role)
                                      .Where(u => u.IsActive)
                                      .AsQueryable();
            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(u => u.FullName.Contains(search));
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
            user.IsActive = false;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
