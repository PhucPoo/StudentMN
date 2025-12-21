using StudentMN.Models.Entities.ScoreStudent;

namespace StudentMN.Repositories.Interface
{
    public interface IScoreRepository
    {
        Task<List<Score>> GetAllScoreAsync();
        Task<Score?> GetScoreByStudentIdAsync(int id);
        Task<Score> AddScoreAsync(Score scoreEntity);
        Task UpdateScoreAsync(Score scoreEntity);
        Task<bool> ExistsAsync(int id);
    }
}
