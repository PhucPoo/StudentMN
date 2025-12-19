using StudentMN.Models.Entities.Class;

namespace StudentMN.Repositories.Interfaces
{
    public interface IClassRepository
    {
        Task<List<Classes>> GetAllClassAsync();
        Task<Classes?> GetClassByIdAsync(int id);
        Task AddClassAsync(Classes classEntity);
        Task UpdateClassAsync(Classes classEntity);
        Task DeleteClassAsync(Classes classEntity);
        Task<bool> ExistsAsync(int id);
    }
}
