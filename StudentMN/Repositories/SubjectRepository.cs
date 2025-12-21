using Microsoft.EntityFrameworkCore;
using StudentMN.Data;
using StudentMN.Models.Entities.ScoreStudent;
using StudentMN.Repositories.Interface;

namespace StudentMN.Repositories
{
    public class SubjectRepository: ISubjectRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<SubjectRepository> _logger;

        public SubjectRepository(AppDbContext context, ILogger<SubjectRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<Subject>> GetAllSubjectAsync()
        {
            return await _context.Subjects
                                 .Include(c => c.Major)
                                 .ToListAsync();
        }

        public async Task<Subject?> GetSubjectByIdAsync(int id)
        {
            return await _context.Subjects
                                 .Include(c => c.Major)
                                 .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Subject> AddSubjectAsync(Subject SubjectEntity)
        {
            if (SubjectEntity == null)
            {
                _logger.LogError("AddSubjectAsync called with null SubjectEntity");
                throw new ArgumentNullException(nameof(SubjectEntity));
            }

            if (await _context.Subjects.AnyAsync(s => s.SubjectCode == SubjectEntity.SubjectCode))
            {
                _logger.LogWarning(
                    "SubjectCode {SubjectCode} already exists",
                    SubjectEntity.SubjectCode
                );

                throw new Exception("SubjectCode already exists; a new Subject cannot be created.");
            }


            await _context.Subjects.AddAsync(SubjectEntity);

            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation(
                    "Subject created successfully. SubjectCode: {SubjectCode}",
                    SubjectEntity.SubjectCode
                );
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(
                    ex,
                    "Error while saving Subject. SubjectCode: {SubjectCode}",
                    SubjectEntity.SubjectCode
                );
                throw;
            }

            return SubjectEntity;
        }

        public async Task UpdateSubjectAsync(Subject SubjectEntity)
        {
            _context.Subjects.Update(SubjectEntity);
            await _context.SaveChangesAsync();

            _logger.LogInformation(
                "Subject updated successfully. SubjectId: {SubjectId}",
                SubjectEntity.Id
            );
        }

        public async Task DeleteSubjectAsync(Subject SubjectEntity)
        {
            _context.Subjects.Remove(SubjectEntity);
            await _context.SaveChangesAsync();

            _logger.LogInformation(
                "Subject deleted successfully. SubjectId: {SubjectId}",
                SubjectEntity.Id
            );
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Subjects.AnyAsync(c => c.Id == id);
        }
    }
}
