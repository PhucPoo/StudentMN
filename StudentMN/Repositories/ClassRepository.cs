using Microsoft.EntityFrameworkCore;
using StudentMN.Data;
using StudentMN.Models.Entities.Class;
using StudentMN.Repositories.Interfaces;

namespace StudentMN.Repositories
{
    public class ClassRepository : IClassRepository
    {
        private readonly AppDbContext _context;

        public ClassRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Classes>> GetAllClassAsync()
        {
            return await _context.Classes
                                 .Include(c => c.Teacher)
                                    .ThenInclude(t => t.User)
                                 .Include(c => c.Major)
                                 .ToListAsync();
        }

        public async Task<Classes?> GetClassByIdAsync(int id)
        {
            return await _context.Classes
                                 .Include(c => c.Teacher)
                                    .ThenInclude(t => t.User)
                                 .Include(c => c.Major)
                                 .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddClassAsync(Classes classEntity)
        {
            await _context.Classes.AddAsync(classEntity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateClassAsync(Classes classEntity)
        {
            _context.Classes.Update(classEntity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteClassAsync(Classes classEntity)
        {
            _context.Classes.Remove(classEntity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Classes.AnyAsync(c => c.Id == id);
        }
    }
}
