using StudentMN.Models.Entities.ScoreStudent;

namespace StudentMN.Repositories.Interface
{
    public interface IScoreRepository
    {
        Task<List<Score>> GetAllScoreAsync();
        Task<Score?> GetScoresByStudentAsync(int courseSectionId);
        Task<Score> AddScoresAsync(Score scoreEntity);
        Task UpdateScoreAsync(Score score);
        Task<bool> ExistsAsync(int studentId, int courseSectionId);
    }
}
