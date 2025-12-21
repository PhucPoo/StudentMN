using StudentMN.Models.Entities.ScoreStudent;

namespace StudentMN.Repositories.Interface
{
    public interface ISubjectRepository
    {
        Task<List<Subject>> GetAllSubjectAsync();
        Task<Subject?> GetSubjectByIdAsync(int id);
        Task<Subject> AddSubjectAsync(Subject SubjectEntity);
        Task UpdateSubjectAsync(Subject SubjectEntity);
        Task DeleteSubjectAsync(Subject SubjectEntity);
        Task<bool> ExistsAsync(int id);
    }
}
