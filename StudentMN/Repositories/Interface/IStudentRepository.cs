using StudentMN.Models.Entities.Account;

namespace StudentMN.Repositories.Interface
{
    public interface IStudentRepository
    {
        Task<List<Student>> GetAllStudentAsync();
        Task<Student?> GetStudentByIdAsync(int id);
        Task<Student> AddStudentAsync(Student studentEntity);
        Task UpdateStudentAsync(Student studentEntity);
        Task DeleteStudentAsync(Student studentEntity);
        Task<bool> ExistsAsync(int id);
    }
}
