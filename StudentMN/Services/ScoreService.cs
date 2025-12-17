using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudentMN.Data;
using StudentMN.DTOs.Request;
using StudentMN.DTOs.Response;
using StudentMN.Models.Account;
using StudentMN.Models.Class;
using StudentMN.Models.ScoreStudent;
using StudentMN.Services.AuthService;

namespace StudentMN.Services
{
    public class ScoreService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public ScoreService(AppDbContext context, IMapper mapper, IAuthService authService)
        {
            _context = context;
            _mapper = mapper;
        }
        // Xem danh sách khoa
        public async Task<PagedResponse<ScoreResponseDTO>> GetAllScoreAsync(int pageNumber = 1, int pageSize = 8, string? search = null)
        {
            var query = _context.Scores.AsQueryable();
            var totalCount = await query.CountAsync();

            var scores = await query
                .OrderBy(s => s.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var scoresDto = _mapper.Map<List<ScoreResponseDTO>>(scores);

            return new PagedResponse<ScoreResponseDTO>
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize),
                Data = scoresDto
            };
        }
        //Thêm khoa mới
        public async Task<ScoreResponseDTO> CreateScoreAsync(ScoreRequestDTO dto)
        {
            var score = _mapper.Map<Score>(dto);

            _context.Scores.Add(score);
            await _context.SaveChangesAsync();
            return _mapper.Map<ScoreResponseDTO>(score);
        }

        //Cập nhật khoa mới
        public async Task<ScoreResponseDTO?> UpdateScoreAsync(int id, ScoreRequestDTO dto)
        {
            var score = await _context.Scores.FindAsync(id);
            if (score == null) return null;

            _mapper.Map(dto, score);
            await _context.SaveChangesAsync();
            return _mapper.Map<ScoreResponseDTO>(score);
        }
        public async Task<bool> DeleteScoreAsync(int id)
        {
            var score = await _context.Scores.FindAsync(id);
            if (score == null) return false;
            score.IsDelete = true;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
