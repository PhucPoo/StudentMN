using Microsoft.EntityFrameworkCore;
using StudentMN.Data;
using StudentMN.Models.Entities.Account;
using StudentMN.Models.Entities.Class;
using StudentMN.Repositories.Interface;

namespace StudentMN.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<StudentRepository> _logger;

        public StudentRepository(AppDbContext context, ILogger<StudentRepository> logger)
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

        public async Task<List<Student>> GetStudentsByClassAsync(int classId)
        {
            return await _context.Students
                                 .Include(c => c.Class)
                                 .Include(c => c.User)
                                 .Where(s => s.ClassId == classId)
                                 .ToListAsync();
        }
        public async Task<Student> GetStudentsByIdAsync(int id)
        {
            return await _context.Students
                                 .Include(c => c.Class)
                                 .Include(c => c.User)
                                 .FirstOrDefaultAsync(s => s.Id == id);

        }

        public async Task<Student> AddStudentAsync(Student studentEntity)
        {
            if (studentEntity == null)
            {
                _logger.LogError("AddStudentAsync called with null studentEntity");
                throw new ArgumentNullException(nameof(studentEntity));
            }

            if (studentEntity.UserId <= 0)
            {
                _logger.LogWarning("Invalid UserId: {UserId}", studentEntity.UserId);
                throw new Exception("UserId invalid");
            }

            if (!await _context.Users.AnyAsync(u => u.Id == studentEntity.UserId))
            {
                _logger.LogWarning("User does not exist. UserId: {UserId}", studentEntity.UserId);
                throw new Exception("User does not exist");
            }

            if (await _context.Students.AnyAsync(s => s.UserId == studentEntity.UserId))
            {
                _logger.LogWarning(
                    "UserId {UserId} is already used for another Student",
                    studentEntity.UserId
                );
                throw new Exception("User used for another student");
            }

            await _context.Students.AddAsync(studentEntity);

            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation(
                    "Student created successfully. StudentId: {StudentId}, UserId: {UserId}",
                    studentEntity.Id,
                    studentEntity.UserId
                );
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(
                    ex,
                    "Error while saving Student. UserId: {UserId}",
                    studentEntity.UserId
                );
                throw;
            }

            return studentEntity;
        }

        public async Task UpdateStudentAsync(Student studentEntity)
        {
            _context.Students.Update(studentEntity);
            await _context.SaveChangesAsync();

            _logger.LogInformation(
                "Student updated successfully. StudentId: {StudentId}",
                studentEntity.Id
            );
        }

        public async Task DeleteStudentAsync(Student studentEntity)
        {
            _context.Students.Remove(studentEntity);
            await _context.SaveChangesAsync();

            _logger.LogInformation(
                "Student deleted successfully. StudentId: {StudentId}",
                studentEntity.Id
            );
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Students.AnyAsync(c => c.Id == id);
        }
    }
}
