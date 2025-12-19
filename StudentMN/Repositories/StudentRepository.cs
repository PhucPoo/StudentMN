using Microsoft.EntityFrameworkCore;
using StudentMN.Data;
using StudentMN.Models.Entities.Account;
using StudentMN.Repositories.Interface;

namespace StudentMN.Repositories
{
    public class StudentRepository:IStudentRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<StudentRepository> _logger;

        public StudentRepository(AppDbContext context,ILogger<StudentRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<Student>> GetAllStudentAsync()
        {
            return await _context.Students
                                 .Include(c => c.Class)
                                 .Include(c => c.User)
                                 .ToListAsync();
        }

        public async Task<Student?> GetStudentByIdAsync(int id)
        {
            return await _context.Students
                                 .Include(c => c.Class)
                                 .Include(c => c.User)
                                 .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Student> AddStudentAsync(Student studentEntity)
        {
           
            var isExist= _context.Students.Any(s => s.UserId == studentEntity.Id);
            if (isExist)
            {
                _logger.LogWarning("Cannot create student because a student with UserId {UserId} already exists.", studentEntity.UserId);
                return null;
            }
            await _context.Students.AddAsync(studentEntity);
            await _context.SaveChangesAsync();
            return studentEntity;
        }

        public async Task UpdateStudentAsync(Student studentEntity)
        {
            _context.Students.Update(studentEntity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteStudentAsync(Student studentEntity)
        {
            _context.Students.Remove(studentEntity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Students.AnyAsync(c => c.Id == id);
        }
    }
}
