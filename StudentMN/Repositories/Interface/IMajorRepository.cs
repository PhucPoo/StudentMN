using StudentMN.Models.Entities.Class;

namespace StudentMN.Repositories.Interface
{
    public interface IMajorRepository
    {
        Task<List<Major>> GetAllMajorAsync();
        Task<Major?> GetMajorByIdAsync(int id);
        Task<Major> AddMajorAsync(Major MajorEntity);
        Task UpdateMajorAsync(Major MajorEntity);
        Task DeleteMajorAsync(Major MajorEntity);
        Task<bool> ExistsAsync(int id);
    }
}
