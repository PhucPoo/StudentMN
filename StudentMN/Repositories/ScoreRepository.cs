using Microsoft.EntityFrameworkCore;
using StudentMN.Data;
using StudentMN.DTOs.Response;
using StudentMN.Models.Entities.ScoreStudent;
using StudentMN.Repositories.Interface;
using StudentMN.Services.Interfaces;

namespace StudentMN.Repositories
{
    public class ScoreRepository:IScoreRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<ScoreRepository> _logger;

        public ScoreRepository(AppDbContext context, ILogger<ScoreRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<Score>> GetAllScoreAsync()
        {
            return await _context.Scores
                                 .Include(c => c.Subject)
                                 .Include(c => c.Student)
                                 .Include(c => c.CourseSection)
                                 .ToListAsync();
        }

        public async Task<Score?> GetScoreByStudentIdAsync(int id)
        {
            return await _context.Scores
                                 .Include(c => c.Subject)
                                 .Include(c => c.Student)
                                 .Include(c => c.CourseSection)
                                 .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task UpdateScoreAsync(Score updatedScore)
        {
            // Lấy score hiện tại từ DB
            var score = await _context.Scores
                .FirstOrDefaultAsync(s => s.Id == updatedScore.Id);

            if (score == null)
                throw new Exception("Score not found");

            // Cập nhật từng phần điểm nếu có
            if (updatedScore.AttendanceScore.HasValue)
                score.AttendanceScore = updatedScore.AttendanceScore.Value;

            if (updatedScore.MidtermScore.HasValue)
                score.MidtermScore = updatedScore.MidtermScore.Value;

            if (updatedScore.FinalScore.HasValue)
                score.FinalScore = updatedScore.FinalScore.Value;


            // Tự động tính điểm trung bình nếu cả 3 điểm đều có
            if (score.AttendanceScore.HasValue &&
                score.MidtermScore.HasValue &&
                score.FinalScore.HasValue)
            {
                score.AverageScore = (score.AttendanceScore.Value * 0.1f +
                                      score.MidtermScore.Value * 0.3f +
                                      score.FinalScore.Value * 0.6f);
            }

            _context.Scores.Update(score);
            await _context.SaveChangesAsync();

            _logger.LogInformation(
                "Score updated successfully. ScoreId: {ScoreId}",
                score.Id
            );
        }


        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Scores.AnyAsync(c => c.Id == id);
        }
    }
}
