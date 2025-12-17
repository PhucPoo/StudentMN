using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudentMN.Data;
using StudentMN.DTOs.Request;
using StudentMN.DTOs.Response;
using StudentMN.Models.Entities.Class;
using StudentMN.Services.Interfaces;


namespace StudentMN.Services
{
    public class CourseSectionService :ICourseSectionService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public CourseSectionService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        // Xem danh sách lớp học phần
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
       
        //Thêm lớp học phần mới
        public async Task<CourseSectionResponseDTO> CreateCourseSectionAsync(CourseSectionRequestDTO dto)
        {
            var courseSection = _mapper.Map<CourseSection>(dto);

            _context.CourseSections.Add(courseSection);
            await _context.SaveChangesAsync();
            return _mapper.Map<CourseSectionResponseDTO>(courseSection);
        }

        //Cập nhật lớp học phần mới
        public async Task<CourseSectionResponseDTO> UpdateCourseSectionAsync(int id, CourseSectionRequestDTO courseSection)
        {
            var coursesection = await _context.CourseSections.FindAsync(id);
            if (coursesection == null || coursesection.IsDelete) return null;

            _mapper.Map(courseSection, coursesection);
            await _context.SaveChangesAsync();
            return _mapper.Map<CourseSectionResponseDTO>(coursesection);

        }
        //Xóa lớp học phần mới
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
