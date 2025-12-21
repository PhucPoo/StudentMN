using StudentMN.Models.Entities.Account;

namespace StudentMN.Repositories.Interface
{
    public interface ITeacherRepository
    {
        Task<List<Teacher>> GetAllTeacherAsync();
        Task<Teacher?> GetTeacherByIdAsync(int id);
        Task<Teacher> AddTeacherAsync(Teacher teacherEntity);
        Task UpdateTeacherAsync(Teacher teacherEntity);
        Task DeleteTeacherAsync(Teacher teacherEntity);
        Task<bool> ExistsAsync(int id);
    }
}
