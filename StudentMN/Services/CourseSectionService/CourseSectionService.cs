using AutoMapper;
using StudentMN.Data;
using StudentMN.DTOs.Request;
using StudentMN.DTOs.Response;
using StudentMN.Models.Class;
using StudentMN.Services.AuthService;
using Microsoft.EntityFrameworkCore;


namespace StudentMN.Services.CourseSectionService
{
    public class CourseSectionService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public CourseSectionService(AppDbContext context, IMapper mapper, ILogger logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }
        // Xem danh sách khoa
        public async Task<PagedResponse<CourseSectionResponseDTO>> GetAllCourseSectionAsync(int pageNumber = 1, int pageSize = 8, string? search = null)
        {
            var query = _context.CourseSections.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(s => s.SectionCode.Contains(search));
            }

            var totalCount = await query.CountAsync();

            var courseSections = await query
                .OrderBy(s => s.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var courseSectionsDto = _mapper.Map<List<CourseSectionResponseDTO>>(courseSections);

            return new PagedResponse<CourseSectionResponseDTO>
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize),
                Data = courseSectionsDto
            };
        }
        //Thêm khoa mới
        public async Task<CourseSectionResponseDTO> CreateCourseSectionAsync(CourseSectionRequestDTO dto)
        {
            var courseSection = _mapper.Map<CourseSection>(dto);

            _context.CourseSections.Add(courseSection);
            await _context.SaveChangesAsync();
            return _mapper.Map<CourseSectionResponseDTO>(courseSection);
        }

        //Cập nhật khoa mới
        public async Task<CourseSectionResponseDTO?> UpdateCourseSectionAsync(int id, CourseSectionRequestDTO dto)
        {
            var courseSection = await _context.CourseSections.FindAsync(id);
            if (courseSection == null) return null;

            _mapper.Map(dto, courseSection);
            await _context.SaveChangesAsync();
            return _mapper.Map<CourseSectionResponseDTO>(courseSection);
        }
        public async Task<bool> DeleteCourseSectionAsync(int id)
        {
            var courseSection = await _context.CourseSections.FindAsync(id);
            if (courseSection == null) return false;
            courseSection.IsDelete = true;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
