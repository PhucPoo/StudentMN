using Microsoft.EntityFrameworkCore;
using StudentMN.Data;
using StudentMN.Models.Entities.ScoreStudent;
using StudentMN.Repositories.Interface;

namespace StudentMN.Repositories
{
    public class ScoreRepository : IScoreRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<ScoreRepository> _logger;

        public ScoreRepository(AppDbContext context, ILogger<ScoreRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Lấy tất cả điểm
        public async Task<List<Score>> GetAllScoreAsync()
        {
            return await _context.Scores
                                 .Include(c => c.Student)
                                 .Include(c => c.CourseSection)
                                    .ThenInclude(cs => cs.Subject)
                                 .ToListAsync();
        }

        // Lấy điểm của student cho 1 môn
        public async Task<Score?> GetScoresByStudentAsync(int studentId)
        {
            return await _context.Scores
                                 .Include(c => c.Student)
                                 .Include(c => c.CourseSection)
                                    .ThenInclude(cs => cs.Subject)
                                 .FirstOrDefaultAsync(c => c.StudentId == studentId );
        }
        public async Task<Score> AddScoresAsync(Score scoreEntity)
        {
            if (scoreEntity == null)
            {
                _logger.LogError("AddScoreAsync called with null scoreEntity");
                throw new ArgumentNullException(nameof(scoreEntity));
            }
            var courseSection = await _context.CourseSections.FindAsync(scoreEntity.CourseSectionId);
            if (courseSection == null)
            {
                _logger.LogError("CourseSection not found with Id {CourseSectionId}", scoreEntity.CourseSectionId);
                throw new Exception("CourseSection not found");
            }
            scoreEntity.CourseSection = courseSection;
            if (scoreEntity.CourseSection == null)
            {
                _logger.LogError("CourseSection is null for StudentId {StudentId}", scoreEntity.StudentId);
                throw new ArgumentNullException(nameof(scoreEntity.CourseSection), "CourseSection cannot be null");
            }

            // Kiểm tra xem điểm cho student + subject đã tồn tại chưa
            if (await _context.Scores.AnyAsync(s => s.StudentId == scoreEntity.StudentId && s.CourseSection.SubjectId == scoreEntity.CourseSection.SubjectId))
            {
                _logger.LogWarning(
                    "Score for StudentId {StudentId} and SubjectId {SubjectId} already exists",
                    scoreEntity.StudentId, scoreEntity.CourseSection.SubjectId
                );
                throw new Exception("Score for this student and subject already exists.");
            }

            await _context.Scores.AddAsync(scoreEntity);

            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation(
                    "Score created successfully. StudentId: {StudentId}, SubjectId: {SubjectId}",
                    scoreEntity.StudentId, scoreEntity.CourseSection.SubjectId
                );
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(
                    ex,
                    "Error while saving Score. StudentId: {StudentId}, SubjectId: {SubjectId}",
                    scoreEntity.StudentId, scoreEntity.CourseSection.SubjectId
                );
                throw;
            }

            return scoreEntity;
        }


        // Cập nhật điểm
        public async Task UpdateScoreAsync(Score updatedScore)
        {
            var score = await _context.Scores
                                      .FirstOrDefaultAsync(s => s.StudentId == updatedScore.StudentId
                                                             && s.CourseSection.SubjectId == updatedScore.CourseSection.SubjectId);

            if (score == null)
            {
                throw new Exception("Score not found for this student and subject");
            }

            // Cập nhật từng phần điểm nếu có
            if (updatedScore.AttendanceScore.HasValue)
                score.AttendanceScore = updatedScore.AttendanceScore.Value;

            if (updatedScore.MidtermScore.HasValue)
                score.MidtermScore = updatedScore.MidtermScore.Value;

            if (updatedScore.FinalScore.HasValue)
                score.FinalScore = updatedScore.FinalScore.Value;

            // Tính điểm trung bình
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

            _logger.LogInformation("Score updated successfully | StudentId: {StudentId} | SubjectId: {SubjectId}",
                                   score.StudentId, score.CourseSection.SubjectId);
        }

        // Kiểm tra tồn tại điểm theo studentId + subjectId
        public async Task<bool> ExistsAsync(int studentId, int subjectId)
        {
            return await _context.Scores.AnyAsync(c => c.StudentId == studentId && c.CourseSection.SubjectId == subjectId);
        }
    }
}
