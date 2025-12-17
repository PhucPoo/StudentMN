using AutoMapper;
using StudentMN.Data;
using StudentMN.DTOs.Request;
using StudentMN.DTOs.Response;
using Microsoft.EntityFrameworkCore;
using StudentMN.Models.Entities.Class;
using StudentMN.Services.Interfaces;


namespace StudentMN.Services
{
    public class EnrollmentCourseSectionService:IEnrollmentCourseSection
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public EnrollmentCourseSectionService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        // Xem danh đăng kí học phần
        public async Task<PagedResponse<EnrollmentResponseDTO>> GetAllEnrollmentsAsync(int pageNumber = 1, int pageSize = 8, string? search = null)
        {
            var query = _context.EnrollmentCourseSections.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(s => s.CourseSection.SectionCode.Contains(search));
            }

            var totalCount = await query.CountAsync();

            var enrollments = await query
                .OrderBy(s => s.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var enrollmentsDto = _mapper.Map<List<EnrollmentResponseDTO>>(enrollments);

            return new PagedResponse<EnrollmentResponseDTO>
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize),
                Data = enrollmentsDto
            };
        }
        //Thêm đăng kí lớp học phần mới
        public async Task<EnrollmentResponseDTO> CreateEnrollmentAsync(EnrollmentRequestDTO dto)
        {
            var enrollment = _mapper.Map<EnrollmentCourseSection>(dto);

            _context.EnrollmentCourseSections.Add(enrollment);
            await _context.SaveChangesAsync();
            return _mapper.Map<EnrollmentResponseDTO>(enrollment);
        }

        //Xóa đăng kí học phần 
        public async Task<bool> DeleteEnrollmentAsync(int id)
        {
            var enrollment = await _context.EnrollmentCourseSections.FindAsync(id);
            if (enrollment == null) return false;
            enrollment.IsDelete = true;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
