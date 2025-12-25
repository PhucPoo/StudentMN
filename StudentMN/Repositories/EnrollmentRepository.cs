using Microsoft.EntityFrameworkCore;
using StudentMN.Data;
using StudentMN.Models.Entities.Account;
using StudentMN.Models.Entities.Class;
using StudentMN.Models.Entities.ScoreStudent;
using StudentMN.Repositories.Interface;

namespace StudentMN.Repositories
{
    public class EnrollmentRepository : IEnrollmentRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<EnrollmentRepository> _logger;

        public EnrollmentRepository(AppDbContext context, ILogger<EnrollmentRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<EnrollmentCourseSection>> GetAllEnrollmentAsync()
        {
            return await _context.Enrollments
                    .Include(e => e.Student)
                    .Include(e => e.CourseSection)
                    .ToListAsync();
        }
        public async Task<EnrollmentCourseSection?> GetEnrollmentsByIdAsync(int Id)
        {
            return await _context.Enrollments
                   .Include(e => e.CourseSection)
                   .Include(e => e.Student)
                   .FirstOrDefaultAsync(e => e.Id == Id);
        }

        public async Task<List<EnrollmentCourseSection>> GetEnrollmentsByStudentIdAsync(int studentId)
        {
            return await _context.Enrollments
                .Include(e => e.CourseSection)
                    .ThenInclude(cs => cs.Subject)
                .Where(e => e.StudentId == studentId)
                .ToListAsync();
        }

        public async Task<EnrollmentCourseSection> AddEnrollmentAsync(EnrollmentCourseSection EnrollmentEntity)
        {
            if (EnrollmentEntity == null)
            {
                _logger.LogError("AddEnrollmentAsync called with null EnrollmentEntity");
                throw new ArgumentNullException(nameof(EnrollmentEntity));
            }

            await _context.Enrollments.AddAsync(EnrollmentEntity);

            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation(
                    "Enrollment created successfully. EnrollmentId: {Id}",
                    EnrollmentEntity.Id
                );
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(
                    ex,
                    "Error while saving Enrollment. EnrollmentId: {Id}",
                    EnrollmentEntity.Id
                );
                throw;
            }

            return EnrollmentEntity;
        }

        public async Task UpdateEnrollmentAsync(EnrollmentCourseSection EnrollmentEntity)
        {
            _context.Enrollments.Update(EnrollmentEntity);
            await _context.SaveChangesAsync();

            _logger.LogInformation(
                "Enrollment updated successfully. EnrollmentId: {EnrollmentId}",
                EnrollmentEntity.Id
            );
        }

        public async Task DeleteEnrollmentAsync(EnrollmentCourseSection EnrollmentEntity)
        {
            _context.Enrollments.Remove(EnrollmentEntity);
            await _context.SaveChangesAsync();

            _logger.LogInformation(
                "Enrollment deleted successfully. EnrollmentId: {EnrollmentId}",
                EnrollmentEntity.Id
            );
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Enrollments.AnyAsync(c => c.Id == id);
        }
    }
}
