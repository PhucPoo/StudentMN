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
        //Lấy đăng kí lớp học phần theo student Id
        public async Task<List<EnrollmentResponseDTO?>> GetEnrollmentsByStudentId(int studentId)
        {
            var enrollment = await _enrollmentRepository.GetEnrollmentsByStudentIdAsync(studentId);

            if (enrollment == null) return null;

            var dto = _mapper.Map<List<EnrollmentResponseDTO>>(enrollment);

            return dto;
        }
        //Lấy lớp học phần theo Id
        public async Task<EnrollmentResponseDTO?> GetEnrollmentsById(int Id)
        {
            var enrollment = await _enrollmentRepository.GetEnrollmentsByIdAsync(Id);

            if (enrollment == null) return null;

            var dto = _mapper.Map<EnrollmentResponseDTO>(enrollment);

            return dto;
        }


        //Xóa đăng kí lớp học phần
        public async Task<bool> DeleteEnrollment(int enrollmentId)
        {
            var enrollmentEntity = await _enrollmentRepository.GetEnrollmentsByIdAsync(enrollmentId);
            if (enrollmentEntity == null) return false;

            await _enrollmentRepository.DeleteEnrollmentAsync(enrollmentEntity);
            return true;
        }
    }
}
