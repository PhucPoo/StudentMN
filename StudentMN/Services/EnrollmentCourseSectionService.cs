using AutoMapper;
using StudentMN.DTOs.Request;
using StudentMN.DTOs.Response;
using StudentMN.Models.Entities.Class;
using StudentMN.Repositories.Interface;
using StudentMN.Services.Interfaces;


namespace StudentMN.Services
{
    public class EnrollmentCourseSectionService: IEnrollmentCourseSection
    {
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly IMapper _mapper;
        public EnrollmentCourseSectionService(IEnrollmentRepository enrollmentRepository, IMapper mapper)
        {
            _enrollmentRepository = enrollmentRepository;
            _mapper = mapper;
        }
        // Xem đăng kí lớp học phần
        public async Task<PagedResponse<EnrollmentResponseDTO>> GetAllEnrollments(int pageNumber = 1, int pageSize = 8, string? search = null)
        {
            var enrollment = await _enrollmentRepository.GetAllEnrollmentAsync();


            var totalCount = enrollment.Count;

            var Enrollments = enrollment
                .OrderBy(s => s.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var enrollmentsDto = _mapper.Map<List<EnrollmentResponseDTO>>(Enrollments);

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
        public async Task<EnrollmentResponseDTO> CreateEnrollment(EnrollmentRequestDTO dto)
        {
            var enrollment = _mapper.Map<EnrollmentCourseSection>(dto);
            var enrollmentAdd = await _enrollmentRepository.AddEnrollmentAsync(enrollment);
            if (enrollmentAdd is null)
            {
                return null;
            }
            return _mapper.Map<EnrollmentResponseDTO>(enrollmentAdd);
        }
        //Lấy đăng kí lớp học phần theo Id
        public async Task<EnrollmentResponseDTO?> GetEnrollmentById(int Id)
        {
            var enrollment = await _enrollmentRepository.GetEnrollmentByIdAsync(Id);

            if (enrollment == null) return null;

            var dto = _mapper.Map<EnrollmentResponseDTO>(enrollment);

            return dto;
        }

        //Cập nhật lớp học phần 
        public async Task<EnrollmentResponseDTO?> UpdateEnrollment(int id, EnrollmentRequestDTO dto)
        {
            var enrollmentEntity = await _enrollmentRepository.GetEnrollmentByIdAsync(id);
            if (enrollmentEntity == null) return null;

            _mapper.Map(dto, enrollmentEntity);

            await _enrollmentRepository.UpdateEnrollmentAsync(enrollmentEntity);

            var updatedEnrollment = await _enrollmentRepository.GetEnrollmentByIdAsync(id);

            return _mapper.Map<EnrollmentResponseDTO>(updatedEnrollment);
        }
        //Xóa đăng kí lớp học phần
        public async Task<bool> DeleteEnrollment(int id)
        {
            var enrollmentEntity = await _enrollmentRepository.GetEnrollmentByIdAsync(id);
            if (enrollmentEntity == null) return false;

            await _enrollmentRepository.DeleteEnrollmentAsync(enrollmentEntity);
            return true;
        }
    }
}
