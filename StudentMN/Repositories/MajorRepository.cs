using Microsoft.EntityFrameworkCore;
using StudentMN.Data;
using StudentMN.Models.Entities.Class;
using StudentMN.Repositories.Interface;

namespace StudentMN.Repositories
{
    public class MajorRepository:IMajorRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<MajorRepository> _logger;

        public MajorRepository(AppDbContext context, ILogger<MajorRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<Major>> GetAllMajorAsync()
        {
            return await _context.Majors.ToListAsync();
        }

        public async Task<Major?> GetMajorByIdAsync(int id)
        {
            return await _context.Majors.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Major> AddMajorAsync(Major MajorEntity)
        {
            if (MajorEntity == null)
            {
                _logger.LogError("AddMajorAsync called with null MajorEntity");
                throw new ArgumentNullException(nameof(MajorEntity));
            }

            await _context.Majors.AddAsync(MajorEntity);

            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation(
                    "Major created successfully. MajorId: {MajorId}",
                    MajorEntity.Id
                );
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(
                    ex,
                    "Error while saving Major. Id: {Id}",
                    MajorEntity.Id
                );
                throw;
            }

            return MajorEntity;
        }

        public async Task UpdateMajorAsync(Major MajorEntity)
        {
            _context.Majors.Update(MajorEntity);
            await _context.SaveChangesAsync();

            _logger.LogInformation(
                "Major updated successfully. MajorId: {MajorId}",
                MajorEntity.Id
            );
        }

        public async Task DeleteMajorAsync(Major MajorEntity)
        {
            _context.Majors.Remove(MajorEntity);
            await _context.SaveChangesAsync();

            _logger.LogInformation(
                "Major deleted successfully. MajorId: {MajorId}",
                MajorEntity.Id
            );
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Majors.AnyAsync(c => c.Id == id);
        }
    }
}
