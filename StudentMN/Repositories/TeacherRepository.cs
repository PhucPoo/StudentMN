using Microsoft.EntityFrameworkCore;
using StudentMN.Data;
using StudentMN.Models.Entities.Account;
using StudentMN.Repositories.Interface;

namespace StudentMN.Repositories
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<TeacherRepository> _logger;

        public TeacherRepository(AppDbContext context, ILogger<TeacherRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<Teacher>> GetAllTeacherAsync()
        {
            return await _context.Teachers
                                 .Include(c => c.Major)
                                 .Include(c => c.User)
                                 .ToListAsync();
        }

        public async Task<Teacher?> GetTeacherByIdAsync(int id)
        {
            return await _context.Teachers
                                 .Include(c => c.Major)
                                 .Include(c => c.User)
                                 .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Teacher> AddTeacherAsync(Teacher teacherEntity)
        {
            if (teacherEntity == null)
            {
                _logger.LogError("AddTeacherAsync called with null teacherEntity");
                throw new ArgumentNullException(nameof(teacherEntity));
            }

            if (teacherEntity.UserId <= 0)
            {
                _logger.LogWarning("Invalid UserId: {UserId}", teacherEntity.UserId);
                throw new Exception("UserId invalid");
            }

            if (!await _context.Users.AnyAsync(u => u.Id == teacherEntity.UserId))
            {
                _logger.LogWarning("User does not exist. UserId: {UserId}", teacherEntity.UserId);
                throw new Exception("User does not exist");
            }

            if (await _context.Teachers.AnyAsync(t => t.UserId == teacherEntity.UserId))
            {
                _logger.LogWarning(
                    "UserId {UserId} is already used for another Teacher",
                    teacherEntity.UserId
                );
                throw new Exception("User used for another Teacher");
            }

            await _context.Teachers.AddAsync(teacherEntity);

            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation(
                    "Teacher created successfully. TeacherId: {TeacherId}, UserId: {UserId}",
                    teacherEntity.Id,
                    teacherEntity.UserId
                );
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(
                    ex,
                    "Error while saving Teacher. UserId: {UserId}",
                    teacherEntity.UserId
                );
                throw;
            }

            return teacherEntity;
        }

        public async Task UpdateTeacherAsync(Teacher teacherEntity)
        {
            _context.Teachers.Update(teacherEntity);
            await _context.SaveChangesAsync();

            _logger.LogInformation(
                "Teacher updated successfully. TeacherId: {TeacherId}",
                teacherEntity.Id
            );
        }

        public async Task DeleteTeacherAsync(Teacher teacherEntity)
        {
            _context.Teachers.Remove(teacherEntity);
            await _context.SaveChangesAsync();

            _logger.LogInformation(
                "Teacher deleted successfully. TeacherId: {TeacherId}",
                teacherEntity.Id
            );
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Teachers.AnyAsync(c => c.Id == id);
        }
    }
}

