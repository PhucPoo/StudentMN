using AutoMapper;
using StudentMN.DTOs.Request;
using StudentMN.DTOs.Response;
using StudentMN.Models.Entities.Account;
using StudentMN.Repositories.Interface;
using StudentMN.Services.Interfaces;
namespace StudentMN.Services
{
    public class TeacherService: ITeacherService
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly IMapper _mapper;

        public TeacherService(ITeacherRepository teacherRepository, IMapper mapper)
        {
            _teacherRepository = teacherRepository;
            _mapper = mapper;
        }

        // Xem danh sách giảng viên
        public async Task<PagedResponse<TeacherResponseDTO>> GetAllTeacher(int pageNumber = 1, int pageSize = 8, string? search = null)
        {
            var Teacher = await _teacherRepository.GetAllTeacherAsync();

            if (!string.IsNullOrWhiteSpace(search))
            {
                Teacher = Teacher
                    .Where(c => c.User?.FullName != null &&
                                c.User.FullName.Contains(search))
                    .ToList();
            }

            var totalCount = Teacher.Count;

            var paged = Teacher
                .OrderBy(c => c.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var dto = _mapper.Map<List<TeacherResponseDTO>>(paged);

            return new PagedResponse<TeacherResponseDTO>
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize),
                Data = dto
            };

        }
        //Lấy giáo viên theo Id
        public async Task<TeacherResponseDTO?> GetTeacherById(int Id)
        {
            var teacher = await _teacherRepository.GetTeacherByIdAsync(Id);

            if (teacher == null) return null;

            var dto = _mapper.Map<TeacherResponseDTO>(teacher);

            return dto;
        }


        // Thêm giảng viên mới
        public async Task<TeacherResponseDTO> CreateTeacher(TeacherRequestDTO dto)
        {
            var teacher = _mapper.Map<Teacher>(dto);
            var teacherAdd = await _teacherRepository.AddTeacherAsync(teacher);
            if (teacherAdd is null)
            {
                return null;
            }
            return _mapper.Map<TeacherResponseDTO>(teacherAdd);
        }

        // Cập nhật giảng viên
        public async Task<TeacherResponseDTO?> UpdateTeacher(int id, TeacherRequestDTO dto)
        {
            var teacherEntity = await _teacherRepository.GetTeacherByIdAsync(id);
            if (teacherEntity == null) return null;

            _mapper.Map(dto, teacherEntity);

            await _teacherRepository.UpdateTeacherAsync(teacherEntity);

            var updatedTeacher = await _teacherRepository.GetTeacherByIdAsync(id);

            return _mapper.Map<TeacherResponseDTO>(updatedTeacher);
        }

        // Xóa tài khoản
        public async Task<bool> DeleteTeacher(int id)
        {
            var teacherEntity = await _teacherRepository.GetTeacherByIdAsync(id);
            if (teacherEntity == null) return false;

            await _teacherRepository.DeleteTeacherAsync(teacherEntity);
            return true;
        }
    }
}
