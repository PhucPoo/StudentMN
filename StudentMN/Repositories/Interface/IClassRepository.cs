using StudentMN.Models.Entities.Class;

namespace StudentMN.Repositories.Interfaces
{
    public interface IClassRepository
    {
        Task<List<Classes>> GetAllAsync();
        Task<Classes?> GetByIdAsync(int id);
        Task AddAsync(Classes classEntity);
        Task UpdateAsync(Classes classEntity);
        Task DeleteAsync(Classes classEntity);
        Task<bool> ExistsAsync(int id);
    }
}
