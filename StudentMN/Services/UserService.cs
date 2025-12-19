using AutoMapper;
using Microsoft.Extensions.Logging;
using StudentMN.DTOs.Request;
using StudentMN.DTOs.Response;
using StudentMN.Models.Entities.Account;
using StudentMN.Repositories.Interface;
using StudentMN.Services.Interfaces;

namespace StudentMN.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;
        private readonly ILogger<UserService> _logger;

        public UserService(IUserRepository userRepository, IMapper mapper, IAuthService authService, ILogger<UserService> logger)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _authService = authService;
            _logger = logger;
        }

        // Xem danh sách người dùng
        public async Task<PagedResponse<UserResponseDTO>> GetAllUser(int pageNumber = 1, int pageSize = 8, string? search = null)
        {
            var users = await _userRepository.GetAllUsersAsync();

            if (!string.IsNullOrWhiteSpace(search))
            {
                users = users
                    .Where(u => !string.IsNullOrEmpty(u.FullName) && u.FullName.Contains(search))
                    .ToList();
            }

            var totalCount = users.Count;

            var pagedUsers = users
                .OrderBy(u => u.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var usersDto = _mapper.Map<List<UserResponseDTO>>(pagedUsers);

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
        public async Task<UserResponseDTO> CreateUser(UserRequestDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Password))
            {
                _logger.LogWarning("Create user false: Password empty | Username: {Username}", dto.Username);
                throw new ArgumentException("Password cannot be empty");
            }

            if (await _userRepository.UserExistsAsync(dto.Username))
            {
                _logger.LogWarning("Create user false: User already exists | Username: {Username}", dto.Username);
                throw new ArgumentException("User already exists");
            }

            if (await _userRepository.EmailExistsAsync(dto.Email))
            {
                _logger.LogWarning("Create user false: Email already exists | Email: {Email}", dto.Email);
                throw new ArgumentException("Email already exists ");
            }

            var user = _mapper.Map<User>(dto);
            user.Password = _authService.HashPassword(dto.Password);

            await _userRepository.CreateUserAsync(user);

            _logger.LogInformation("User created successfully | UserId: {UserId} | Username: {Username}", user.Id, user.Username);

            var createdUser = await _userRepository.GetUserByIdAsync(user.Id);

            return _mapper.Map<UserResponseDTO>(createdUser!);
        }

        // Cập nhật tài khoản
        public async Task<UserResponseDTO?> UpdateUser(int id, UserRequestDTO dto)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null) return null;

            _mapper.Map(dto, user);

            if (!string.IsNullOrWhiteSpace(dto.Password))
            {
                user.Password = _authService.HashPassword(dto.Password);
            }

            await _userRepository.UpdateAsync(user);

            var updatedUser = await _userRepository.GetUserByIdAsync(id);
            return _mapper.Map<UserResponseDTO>(updatedUser!);
        }

        // Xóa tài khoản
        public async Task<bool> DeleteUser(int id)
        {
            return await _userRepository.DeleteUserAsync(id);
        }
    }
}
