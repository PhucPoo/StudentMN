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

        public async Task<List<Classes>> GetAllAsync()
        {
            return await _context.Classes
                                 .Include(c => c.Teacher)
                                 .Include(c => c.Major)
                                 .Include(c => c.Students)
                                 .ToListAsync();
        }

        public async Task<Classes?> GetByIdAsync(int id)
        {
            return await _context.Classes
                                 .Include(c => c.Teacher)
                                 .Include(c => c.Major)
                                 .Include(c => c.Students)
                                 .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddAsync(Classes classEntity)
        {
            await _context.Classes.AddAsync(classEntity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Classes classEntity)
        {
            _context.Classes.Update(classEntity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Classes classEntity)
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
