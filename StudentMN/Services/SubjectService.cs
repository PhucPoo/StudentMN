using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudentMN.Data;
using StudentMN.DTOs.Request;
using StudentMN.DTOs.Response;
using StudentMN.Models.Entities.ScoreStudent;
using StudentMN.Services.Interfaces;

namespace StudentMN.Services
{
    public class SubjectService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public SubjectService(AppDbContext context, IMapper mapper, IAuthService authService)
        {
            _context = context;
            _mapper = mapper;
        }
        // Xem danh sách khoa
        public async Task<PagedResponse<SubjectResponseDTO>> GetAllSubjectAsync(int pageNumber = 1, int pageSize = 8, string? search = null)
        {
            var query = _context.Subjects.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(s => s.SubjectName.Contains(search));
            }

            var totalCount = await query.CountAsync();

            var Subjects = await query
                .OrderBy(s => s.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var SubjectsDto = _mapper.Map<List<SubjectResponseDTO>>(Subjects);

            return new PagedResponse<SubjectResponseDTO>
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize),
                Data = SubjectsDto
            };
        }
        //Thêm môn học mới
        public async Task<SubjectResponseDTO> CreateSubjectAsync(SubjectRequestDTO dto)
        {
            var Subject = _mapper.Map<Subject>(dto);

            _context.Subjects.Add(Subject);
            await _context.SaveChangesAsync();
            return _mapper.Map<SubjectResponseDTO>(Subject);
        }

        //Cập nhật môn học mới
        public async Task<SubjectResponseDTO?> UpdateSubjectAsync(int id, SubjectRequestDTO dto)
        {
            var Subject = await _context.Subjects.FindAsync(id);
            if (Subject == null) return null;

            _mapper.Map(dto, Subject);
            await _context.SaveChangesAsync();
            return _mapper.Map<SubjectResponseDTO>(Subject);
        }
        public async Task<bool> DeleteSubjectAsync(int id)
        {
            var Subject = await _context.Subjects.FindAsync(id);
            if (Subject == null) return false;
            Subject.IsDelete = true;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
