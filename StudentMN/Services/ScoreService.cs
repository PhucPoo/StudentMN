using AutoMapper;
using StudentMN.DTOs.Request;
using StudentMN.DTOs.Response;
using StudentMN.Repositories.Interface;
using StudentMN.Services.Interfaces;

namespace StudentMN.Services
{
    public class ScoreService : IScoreService
    {
        private readonly IScoreRepository _scoreRepository;
        private readonly IMapper _mapper;
        public ScoreService(IScoreRepository scoreRepository, IMapper mapper, IAuthService authService)
        {
            _scoreRepository = scoreRepository;
            _mapper = mapper;
        }
        // Xem danh sách khoa
        public async Task<PagedResponse<ScoreResponseDTO>> GetAllScore(int pageNumber = 1, int pageSize = 8, string? search = null)
        {
            var score = await _scoreRepository.GetAllScoreAsync();

            var totalCount = score.Count;

            var Scores = score
                .OrderBy(c => c.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var scoresDto = _mapper.Map<List<ScoreResponseDTO>>(Scores);

            return new PagedResponse<ScoreResponseDTO>
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize),
                Data = scoresDto
            };
        }
        //Lấy khoa theo Id
        public async Task<ScoreResponseDTO?> GetScoreById(int id)
        {
            var score = await _scoreRepository.GetScoreByStudentIdAsync(id);
            if (score == null) return null;

            return _mapper.Map<ScoreResponseDTO>(score);
        }



        // Cập nhật giảng viên
        public async Task<ScoreResponseDTO?> UpdateScore(int id, ScoreRequestDTO dto)
        {
            var scoreEntity = await _scoreRepository.GetScoreByStudentIdAsync(id);
            if (scoreEntity == null) return null;

            _mapper.Map(dto, scoreEntity);

            await _scoreRepository.UpdateScoreAsync(scoreEntity);

            var updatedScore = await _scoreRepository.GetScoreByStudentIdAsync(id);
            if (updatedScore == null) return null;

            return _mapper.Map<ScoreResponseDTO>(updatedScore);
        }



    }
}
