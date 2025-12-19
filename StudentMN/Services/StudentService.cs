using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudentMN.Data;
using StudentMN.DTOs.Request;
using StudentMN.DTOs.Response;
using StudentMN.Models.Entities.Account;
using StudentMN.Models.Entities.Class;
using StudentMN.Repositories.Interface;
using StudentMN.Services.Interfaces;

namespace StudentMN.Services
{
    public class StudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;

        public StudentService(IStudentRepository studentRepository, IMapper mapper, IAuthService authService)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
        }

        // Xem danh sách sinh viên
        public async Task<PagedResponse<StudentResponseDTO>> GetAllStudentAsync(int pageNumber = 1,int pageSize = 8,string? search = null)
        {
            var student = await _studentRepository.GetAllStudentAsync();

            if (!string.IsNullOrWhiteSpace(search))
            {
                student = student
                    .Where(c => c.User?.FullName != null &&
                                c.User.FullName.Contains(search))
                    .ToList();
            }

            var totalCount = student.Count;

            var paged = student
                .OrderBy(c => c.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var dto = _mapper.Map<List<StudentResponseDTO>>(paged);

            return new PagedResponse<StudentResponseDTO>
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize),
                Data = dto
            };

        }
        public async Task<StudentResponseDTO?> GetStudentByIdAsync(int Id)
        {
            var student = await _studentRepository.GetStudentByIdAsync(Id);

            if (student == null) return null;

            var dto = _mapper.Map<StudentResponseDTO>(student);
            dto.Email = student.User?.Email;

            return dto;
        }


        // Thêm sinh viên mới
        public async Task<StudentResponseDTO> CreateStudentAsync(StudentRequestDTO dto)
        {
            var student = _mapper.Map<Student>(dto);
            var studentAdd=await _studentRepository.AddStudentAsync(student);
            if(studentAdd is null)
            {
                return null;
            }
            return _mapper.Map<StudentResponseDTO>(studentAdd);
        }

        // Cập nhật sinh viên
        public async Task<StudentResponseDTO?> UpdateStudentAsync(int id, StudentRequestDTO dto, string role)
        {
            var studentEntity = await _studentRepository.GetStudentByIdAsync(id);
            if (studentEntity == null) return null;

            _mapper.Map(dto, studentEntity);

            await _studentRepository.UpdateStudentAsync(studentEntity);

            var updatedStudent = await _studentRepository.GetStudentByIdAsync(id);

            return _mapper.Map<StudentResponseDTO>(updatedStudent);
        }

        // Xóa tài khoản
        public async Task<bool> DeleteStudentAsync(int id)
        {
            var studentEntity = await _studentRepository.GetStudentByIdAsync(id);
            if (studentEntity == null) return false;

            await _studentRepository.DeleteStudentAsync(studentEntity);
            return true;
        }
    }
}
