using AutoMapper;
using StudentMN.DTOs.Request;
using StudentMN.DTOs.Response;
using StudentMN.Models.Entities.ScoreStudent;
using StudentMN.Repositories;
using StudentMN.Repositories.Interface;
using StudentMN.Services.Interfaces;

namespace StudentMN.Services
{
    public class ScoreService : IScoreService
    {
        private readonly IScoreRepository _scoreRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ScoreService> _logger;

        public ScoreService(ILogger<ScoreService> logger,IScoreRepository scoreRepository, IMapper mapper)
        {
            _scoreRepository = scoreRepository;
            _mapper = mapper;
            _logger = logger;
        }

        // Xem danh sách điểm với paging
        public async Task<PagedResponse<ScoreResponseDTO>> GetAllScore(int pageNumber = 1, int pageSize = 8, string? search = null)
        {
            var scores = await _scoreRepository.GetAllScoreAsync();

            if (!string.IsNullOrWhiteSpace(search))
            {
                scores = scores
                    .Where(s => s.Student != null && s.Student.User != null && s.Student.User.FullName.Contains(search))
                    .ToList();
            }

            var totalCount = scores.Count;

            var pagedScores = scores
                .OrderBy(s => s.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var scoresDto = _mapper.Map<List<ScoreResponseDTO>>(pagedScores);

            return new PagedResponse<ScoreResponseDTO>
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize),
                Data = scoresDto
            };
        }

        // Lấy điểm 1 student theo studentId và subjectId
        public async Task<ScoreResponseDTO?> GetScoresByStudent(int studentId)
        {
            var score = await _scoreRepository.GetScoresByStudentAsync(studentId);
            if (score == null) return null;

            return _mapper.Map<ScoreResponseDTO>(score);
        }
        public async Task<ScoreResponseDTO> AddScore(ScoreRequestDTO dto)
        {
            var score = _mapper.Map<Score>(dto);
            var scoreAdd = await _scoreRepository.AddScoresAsync(score);
            if (scoreAdd is null)
            {
                return null;
            }
            return _mapper.Map<ScoreResponseDTO>(scoreAdd);
        }

        // Cập nhật điểm theo studentId và subjectId
        public async Task<ScoreResponseDTO?> UpdateScore(int studentId, int courseSectionId, ScoreRequestDTO dto)
        {
            // Lấy điểm theo studentId và subjectId
            var scoreEntity = await _scoreRepository.GetScoresByStudentAsync(studentId);

            // Log xem có tìm được hay không
            if (scoreEntity == null)
            {
                _logger.LogWarning("UpdateScore: No score found for StudentId={StudentId}, SubjectId={SubjectId}", studentId, courseSectionId);
                return null;
            }
            else
            {
                _logger.LogInformation("UpdateScore: Found score with Id={ScoreId} for StudentId={StudentId}, SubjectId={SubjectId}",
                                       scoreEntity.Id, studentId, courseSectionId);
            }

            // Áp dụng mapping từ DTO
            _mapper.Map(dto, scoreEntity);

            // Cập nhật điểm
            await _scoreRepository.UpdateScoreAsync(scoreEntity);

            _logger.LogInformation("UpdateScore: Score updated successfully for ScoreId={ScoreId}", scoreEntity.Id);

            return _mapper.Map<ScoreResponseDTO>(scoreEntity);
        }

    }
}
