using Microsoft.EntityFrameworkCore;
using StudentMN.Data;
using StudentMN.Models.Entities.Class;
using StudentMN.Models.Entities.ScoreStudent;
using StudentMN.Repositories.Interface;

namespace StudentMN.Repositories
{
    public class CourseSectionRepository:ICourseSectionRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<CourseSectionRepository> _logger;

        public CourseSectionRepository(AppDbContext context, ILogger<CourseSectionRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<CourseSection>> GetAllCourseSectionAsync()
        {
            return await _context.CourseSections
                                 .Include(c => c.Teacher)
                                    .ThenInclude(c => c.User)
                                 .Include(c => c.Subject)
                                    .ThenInclude(c => c.Major)
                                 .ToListAsync();
        }

        public async Task<CourseSection?> GetCourseSectionByIdAsync(int id)
        {
            return await _context.CourseSections
                                 .Include(c => c.Teacher)
                                 .Include(c => c.Subject)
                                 .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<CourseSection> AddCourseSectionAsync(CourseSection CourseSectionEntity)
        {
            if (CourseSectionEntity == null)
            {
                _logger.LogError("AddCourseSectionAsync called with null CourseSectionEntity");
                throw new ArgumentNullException(nameof(CourseSectionEntity));
            }

            if (await _context.CourseSections.AnyAsync(s => s.SectionCode == CourseSectionEntity.SectionCode))
            {
                _logger.LogWarning(
                    "SectionCode {SectionCode} already exists",
                    CourseSectionEntity.SectionCode
                );

                throw new Exception("SectionCode already exists; a new CourseSection cannot be created.");
            }


            await _context.CourseSections.AddAsync(CourseSectionEntity);

            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation(
                    "CourseSection created successfully. SectionCode: {SectionCode}",
                    CourseSectionEntity.SectionCode
                );
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(
                    ex,
                    "Error while saving CourseSection. SectionCode: {SectionCode}",
                    CourseSectionEntity.SectionCode
                );
                throw;
            }

            return CourseSectionEntity;
        }

        public async Task UpdateCourseSectionAsync(CourseSection CourseSectionEntity)
        {
            _context.CourseSections.Update(CourseSectionEntity);
            await _context.SaveChangesAsync();

            _logger.LogInformation(
                "CourseSection updated successfully. CourseSectionId: {CourseSectionId}",
                CourseSectionEntity.Id
            );
        }

        public async Task DeleteCourseSectionAsync(CourseSection CourseSectionEntity)
        {
            _context.CourseSections.Remove(CourseSectionEntity);
            await _context.SaveChangesAsync();

            _logger.LogInformation(
                "CourseSection deleted successfully. CourseSectionId: {CourseSectionId}",
                CourseSectionEntity.Id
            );
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.CourseSections.AnyAsync(c => c.Id == id);
        }
    }
}
