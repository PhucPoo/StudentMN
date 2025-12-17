using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudentMN.Data;
using StudentMN.DTOs.Request;
using StudentMN.DTOs.Response;
using StudentMN.Models.Entities.Class;
using StudentMN.Services.Interfaces;

namespace StudentMN.Services
{
    public class MajorService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public MajorService(AppDbContext context, IMapper mapper, IAuthService authService)
        {
            _context = context;
            _mapper = mapper;
        }
        // Xem danh sách khoa
        public async Task<PagedResponse<MajorResponseDTO>> GetAllMajorAsync(int pageNumber = 1, int pageSize = 8, string? search = null)
        {
            var query = _context.Majors.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(s => s.MajorName.Contains(search));
            }

            var totalCount = await query.CountAsync();

            var majors = await query
                .OrderBy(s => s.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var majorsDto = _mapper.Map<List<MajorResponseDTO>>(majors);

            return new PagedResponse<MajorResponseDTO>
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize),
                Data = majorsDto
            };
        }
        //Thêm khoa mới
        public async Task<MajorResponseDTO> CreateMajorAsync(MajorRequestDTO dto)
        {
            var major = _mapper.Map<Major>(dto);

            _context.Majors.Add(major);
            await _context.SaveChangesAsync();
            return _mapper.Map<MajorResponseDTO>(major);
        }

        //Cập nhật khoa mới
        public async Task<MajorResponseDTO?> UpdateMajorAsync(int id, MajorRequestDTO dto)
        {
            var major = await _context.Majors.FindAsync(id);
            if (major == null) return null;

            _mapper.Map(dto, major);
            await _context.SaveChangesAsync();
            return _mapper.Map<MajorResponseDTO>(major);
        }
        public async Task<bool> DeleteMajorAsync(int id)
        {
            var major = await _context.Majors.FindAsync(id);
            if (major == null) return false;
            major.IsDelete = true;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
