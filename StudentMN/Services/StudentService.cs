using AutoMapper;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using StudentMN.DTOs.Request;
using StudentMN.DTOs.Response;
using StudentMN.Models.Entities.Account;
using StudentMN.Repositories.Interface;
using StudentMN.Services.Interfaces;
using System.Drawing;

namespace StudentMN.Services
{
    public class StudentService:IStudentService
    {
        private readonly ILogger<StudentService> _logger;
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;

        public StudentService(IStudentRepository studentRepository, IMapper mapper, ILogger<StudentService> logger)
        {
            _studentRepository = studentRepository;
            _logger = logger;
            _mapper = mapper;

        }

        // Xem danh sách sinh viên
        public async Task<PagedResponse<StudentResponseDTO>> GetAllStudent(int pageNumber = 1,int pageSize = 8,string? search = null)
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
        public async Task<List<StudentResponseDTO?>> GetStudentByClass(int classId)
        {
            var students = await _studentRepository.GetStudentsByClassAsync(classId);

            return _mapper.Map<List<StudentResponseDTO>>(students);
        }
        public async Task<byte[]> ExportStudentsByClassToExcel(int classId)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var students = await _studentRepository.GetStudentsByClassAsync(classId);

            using var package = new ExcelPackage();
            var worksheet = package.Workbook.Worksheets.Add("Students");

            worksheet.Cells[1, 1].Value = "StudentCode";
            worksheet.Cells[1, 2].Value = "FullName";
            worksheet.Cells[1, 3].Value = "Email";
            worksheet.Cells[1, 4].Value = "Gender";
            worksheet.Cells[1, 5].Value = "DateOfBirth";
            worksheet.Cells[1, 6].Value = "ClassName";

            using (var range = worksheet.Cells[1, 1, 1, 6])
            {
                range.Style.Font.Bold = true;
                range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                range.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            }

            int row = 2;
            foreach (var s in students)
            {
                worksheet.Cells[row, 1].Value = s.StudentCode;
                worksheet.Cells[row, 2].Value = s.User?.FullName;
                worksheet.Cells[row, 3].Value = s.Course;
                worksheet.Cells[row, 4].Value = s.Gender;
                worksheet.Cells[row, 5].Value = s.DateOfBirth.ToString("dd/MM/yyyy");
                worksheet.Cells[row, 6].Value = s.Class?.ClassName;
                row++;
            }

            worksheet.Cells.AutoFitColumns();

            return package.GetAsByteArray();
        }


        // Thêm sinh viên mới
        public async Task<StudentResponseDTO> CreateStudent(StudentRequestDTO dto)
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
        public async Task<StudentResponseDTO?> UpdateStudent(int id, StudentRequestDTO dto)
        {
            try
            {
                var studentEntity = await _studentRepository.GetStudentsByIdAsync(id);
                if (studentEntity == null)
                {
                    _logger.LogWarning("UpdateStudent: Student with Id {StudentId} not found.", id);
                    return null;
                }

                _mapper.Map(dto, studentEntity);

                await _studentRepository.UpdateStudentAsync(studentEntity);

                var updatedStudent = await _studentRepository.GetStudentsByIdAsync(id);

                return _mapper.Map<StudentResponseDTO>(updatedStudent);
            }
            catch (Exception ex)
            {
                
                _logger.LogError(ex, "UpdateStudent failed for Id {StudentId}", id);
                throw; 
            }
        }


        // Xóa tài khoản
        public async Task<bool> DeleteStudent(int id)
        {
            var studentEntity = await _studentRepository.GetStudentsByIdAsync(id);
            if (studentEntity == null) return false;

            await _studentRepository.DeleteStudentAsync(studentEntity);
            return true;
        }
    }
}
