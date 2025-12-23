using AutoMapper;
using StudentMN.DTOs.Request;
using StudentMN.DTOs.Response;
using StudentMN.Models.Entities.Class;
using StudentMN.Repositories.Interface;
using StudentMN.Services.Interfaces;


namespace StudentMN.Services
{
    public class CourseSectionService :ICourseSectionService
    {
        private readonly ICourseSectionRepository _courseSectionRepository;
        private readonly IMapper _mapper;
        public CourseSectionService(ICourseSectionRepository courseSectionRepository, IMapper mapper)
        {
            _courseSectionRepository = courseSectionRepository;
            _mapper = mapper;
        }
        // Xem danh sách lớp học phần
        public async Task<PagedResponse<CourseSectionResponseDTO>> GetAllCourseSection(int pageNumber = 1, int pageSize = 8, string? search = null)
        {
            var courseSection = await _courseSectionRepository.GetAllCourseSectionAsync();

            if (!string.IsNullOrWhiteSpace(search))
            {
                courseSection = courseSection
                    .Where(c => c.SectionCode != null &&
                                c.SectionCode.Contains(search))
                    .ToList();
            }

            var totalCount = courseSection.Count;

            var Sections = courseSection
                .OrderBy(s => s.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var courseSectionsDto = _mapper.Map<List<CourseSectionResponseDTO>>(Sections);

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
        public async Task<CourseSectionResponseDTO> CreateCourseSection(CourseSectionRequestDTO dto)
        {
            var courseSection = _mapper.Map<CourseSection>(dto);
            var courseSectionAdd = await _courseSectionRepository.AddCourseSectionAsync(courseSection);
            if (courseSectionAdd is null)
            {
                return null;
            }
            return _mapper.Map<CourseSectionResponseDTO>(courseSectionAdd);
        }
        //Lấy lớp học phần theo Id
        public async Task<CourseSectionResponseDTO?> GetCourseSectionById(int Id)
        {
            var CourseSection = await _courseSectionRepository.GetCourseSectionByIdAsync(Id);

            if (CourseSection == null) return null;

            var dto = _mapper.Map<CourseSectionResponseDTO>(CourseSection);

            return dto;
        }

        //Cập nhật lớp học phần 
        public async Task<CourseSectionResponseDTO?> UpdateCourseSection(int id, CourseSectionRequestDTO dto)
        {
            var CourseSectionEntity = await _courseSectionRepository.GetCourseSectionByIdAsync(id);
            if (CourseSectionEntity == null) return null;

            _mapper.Map(dto, CourseSectionEntity);

            await _courseSectionRepository.UpdateCourseSectionAsync(CourseSectionEntity);

            var updatedCourseSection = await _courseSectionRepository.GetCourseSectionByIdAsync(id);

            return _mapper.Map<CourseSectionResponseDTO>(updatedCourseSection);
        }
        //Xóa lớp học phần
        public async Task<bool> DeleteCourseSection(int id)
        {
            var courseSectionEntity = await _courseSectionRepository.GetCourseSectionByIdAsync(id);
            if (courseSectionEntity == null) return false;

            await _courseSectionRepository.DeleteCourseSectionAsync(courseSectionEntity);
            return true;
        }
    }
}
