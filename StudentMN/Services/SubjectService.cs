using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudentMN.Data;
using StudentMN.DTOs.Request;
using StudentMN.DTOs.Response;
using StudentMN.Models.Entities.Account;
using StudentMN.Models.Entities.ScoreStudent;
using StudentMN.Repositories;
using StudentMN.Repositories.Interface;
using StudentMN.Services.Interfaces;

namespace StudentMN.Services
{
    public class SubjectService:ISubjectService
    {
        private readonly ISubjectRepository _subjectRepository;
        private readonly IMapper _mapper;
        public SubjectService(ISubjectRepository subjectRepository, IMapper mapper)
        {
            _subjectRepository = subjectRepository;
            _mapper = mapper;
        }
        // Xem danh sách khoa
        public async Task<PagedResponse<SubjectResponseDTO>> GetAllSubject(int pageNumber = 1, int pageSize = 8, string? search = null)
        {
            var subject = await _subjectRepository.GetAllSubjectAsync();

            if (!string.IsNullOrWhiteSpace(search))
            {
                subject = subject
                    .Where(c => c.SubjectName != null &&
                                c.SubjectName.Contains(search))
                    .ToList();
            }

            var totalCount = subject.Count;

            var Subjects = subject
                .OrderBy(s => s.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

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
        public async Task<SubjectResponseDTO> CreateSubject(SubjectRequestDTO dto)
        {
            var subject = _mapper.Map<Subject>(dto);
            var subjectAdd = await _subjectRepository.AddSubjectAsync(subject);
            if (subjectAdd is null)
            {
                return null;
            }
            return _mapper.Map<SubjectResponseDTO>(subjectAdd);
        }
        //Lấy môn học theo Id
        public async Task<SubjectResponseDTO?> GetSubjectById(int Id)
        {
            var Subject = await _subjectRepository.GetSubjectByIdAsync(Id);

            if (Subject == null) return null;

            var dto = _mapper.Map<SubjectResponseDTO>(Subject);

            return dto;
        }

        //Cập nhật môn học mới
        public async Task<SubjectResponseDTO?> UpdateSubject(int id, SubjectRequestDTO dto)
        {
            var subjectEntity = await _subjectRepository.GetSubjectByIdAsync(id);
            if (subjectEntity == null) return null;

            _mapper.Map(dto, subjectEntity);

            await _subjectRepository.UpdateSubjectAsync(subjectEntity);

            var updatedSubject = await _subjectRepository.GetSubjectByIdAsync(id);

            return _mapper.Map<SubjectResponseDTO>(updatedSubject);
        }
        public async Task<bool> DeleteSubject(int id)
        {
            var subjectEntity = await _subjectRepository.GetSubjectByIdAsync(id);
            if (subjectEntity == null) return false;

            await _subjectRepository.DeleteSubjectAsync(subjectEntity);
            return true;
        }
    }
}
