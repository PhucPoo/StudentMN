//using Microsoft.EntityFrameworkCore;
//using StudentMN.Data;
//using StudentMN.Models.Entities.ScoreStudent;

//namespace StudentMN.Repositories
//{
//    public class ScoreRepository
//    {
//        private readonly AppDbContext _context;
//        private readonly ILogger<ScoreRepository> _logger;

//        public ScoreRepository(AppDbContext context, ILogger<ScoreRepository> logger)
//        {
//            _context = context;
//            _logger = logger;
//        }

//        public async Task<List<Score>> GetAllScoreAsync()
//        {
//            return await _context.Scores
//                                 .Include(c => c.Subject)
//                                 .Include(c => c.Student)
//                                 .Include(c => c.CourseSection)
//                                 .ToListAsync();
//        }

//        public async Task<Score?> GetScoreByStudentIdAsync(int id)
//        {
//            return await _context.Scores
//                                 .Include(c => c.Subject)
//                                 .Include(c => c.Student)
//                                 .Include(c => c.CourseSection)
//                                 .FirstOrDefaultAsync(c => c.Id == id);
//        }

//        public async Task<Score> AddScoreAsync(Score ScoreEntity)
//        {
//            if (ScoreEntity == null)
//            {
//                _logger.LogError("AddScoreAsync called with null ScoreEntity");
//                throw new ArgumentNullException(nameof(ScoreEntity));
//            }

//            if (!await _context.Users.AnyAsync(u => u.Id == ScoreEntity.UserId))
//            {
//                _logger.LogWarning("User does not exist. UserId: {UserId}", ScoreEntity.UserId);
//                throw new Exception("User does not exist");
//            }

//            if (await _context.Scores.AnyAsync(t => t.UserId == ScoreEntity.UserId))
//            {
//                _logger.LogWarning(
//                    "UserId {UserId} is already used for another Score",
//                    ScoreEntity.UserId
//                );
//                throw new Exception("User used for another Score");
//            }

//            await _context.Scores.AddAsync(ScoreEntity);

//            try
//            {
//                await _context.SaveChangesAsync();
//                _logger.LogInformation(
//                    "Score created successfully. ScoreId: {ScoreId}, UserId: {UserId}",
//                    ScoreEntity.Id,
//                    ScoreEntity.UserId
//                );
//            }
//            catch (DbUpdateException ex)
//            {
//                _logger.LogError(
//                    ex,
//                    "Error while saving Score. UserId: {UserId}",
//                    ScoreEntity.UserId
//                );
//                throw;
//            }

//            return ScoreEntity;
//        }

//        public async Task UpdateScoreAsync(Score ScoreEntity)
//        {
//            _context.Scores.Update(ScoreEntity);
//            await _context.SaveChangesAsync();

//            _logger.LogInformation(
//                "Score updated successfully. ScoreId: {ScoreId}",
//                ScoreEntity.Id
//            );
//        }


//        public async Task<bool> ExistsAsync(int id)
//        {
//            return await _context.Scores.AnyAsync(c => c.Id == id);
//        }
//    }
//}
