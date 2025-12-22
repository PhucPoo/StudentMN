using StudentMN.Models.Entities.Account;

namespace StudentMN.Repositories.Interface
{
    public interface IStudentRepository
    {
        Task<List<Student>> GetAllStudentAsync();
        Task<List<Student?>> GetStudentsByClassAsync(int classId);
        Task<Student?> GetStudentsByIdAsync(int classId);
        Task<Student> AddStudentAsync(Student studentEntity);
        Task UpdateStudentAsync(Student studentEntity);
        Task DeleteStudentAsync(Student studentEntity);
        Task<bool> ExistsAsync(int id);
    }
}
