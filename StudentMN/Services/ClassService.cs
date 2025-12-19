using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudentMN.Data;
using StudentMN.DTOs.Request;
using StudentMN.DTOs.Response;
using StudentMN.Models.Entities.Class;
using StudentMN.Models.Entities.PermissionModels;
using StudentMN.Repositories.Interfaces;
using StudentMN.Services.Interfaces;

namespace StudentMN.Services
{
    public class ClassService
    {
        private readonly IClassRepository _classRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ClassService> _logger;
        public ClassService( IMapper mapper, IClassRepository classRepository, IAuthService authService,ILogger<ClassService> logger)
        {
            _classRepository = classRepository;
            _mapper = mapper;
            _logger = logger;
        }
        // Xem danh sách lớp
        public async Task<PagedResponse<ClassesResponseDTO>> GetAllClassAsync(int pageNumber = 1, int pageSize = 8, string? search = null)
        {
            var classes = await _classRepository.GetAllClassAsync();

            if (!string.IsNullOrWhiteSpace(search))
            {
                classes = classes
                    .Where(c => c.ClassName != null &&
                                c.ClassName.Contains(search))
                    .ToList();
            }

            var totalCount = classes.Count;

            var paged = classes
           .OrderBy(c => c.Id)
           .Skip((pageNumber - 1) * pageSize)
           .Take(pageSize)
           .ToList();

            var dto = _mapper.Map<List<ClassesResponseDTO>>(paged);

            return new PagedResponse<ClassesResponseDTO>
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize),
                Data = dto
            };

        }
        //Thêm lớp mới
        public async Task<ClassesResponseDTO> CreateClassAsync(ClassesRequestDTO dto)
        {
            //var teacherExists = await _classRepository.GetTeacherByIdAsync(dto.TeacherId);
            //if (teacherExists == null)
            //    throw new Exception("Teacher does not exist");
            var Class = _mapper.Map<Classes>(dto);
            await _classRepository.AddClassAsync(Class);
            var createdClass = await _classRepository.GetClassByIdAsync(Class.Id);
            if (createdClass == null)
                throw new Exception("Create class failed");

            return _mapper.Map<ClassesResponseDTO>(createdClass);

        }

        //Cập nhật lớp mới
        public async Task<ClassesResponseDTO?> UpdateClassAsync(int id, ClassesRequestDTO dto)
        {
            var classEntity = await _classRepository.GetClassByIdAsync(id);
            if (classEntity == null) return null;

            _mapper.Map(dto, classEntity);

            await _classRepository.UpdateClassAsync(classEntity);

            var updatedClass = await _classRepository.GetClassByIdAsync(id);

            return _mapper.Map<ClassesResponseDTO>(updatedClass);
        }
        //Xóa lớp
        public async Task<bool> DeleteClassAsync(int id)
        {
            var classEntity = await _classRepository.GetClassByIdAsync(id);
            if (classEntity == null) return false;

            await _classRepository.DeleteClassAsync(classEntity);
            return true;
        }
    }
}
