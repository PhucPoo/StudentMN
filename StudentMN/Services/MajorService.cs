using AutoMapper;
using StudentMN.DTOs.Request;
using StudentMN.DTOs.Response;
using StudentMN.Models.Entities.Class;
using StudentMN.Repositories.Interface;
using StudentMN.Services.Interfaces;

namespace StudentMN.Services
{
    public class MajorService: IMajorService
    {
        private readonly IMajorRepository _majorRepository;
        private readonly IMapper _mapper;
        public MajorService(IMajorRepository majorRepository, IMapper mapper)
        {
            _majorRepository = majorRepository;
            _mapper = mapper;
        }
        // Xem danh sách khoa
        public async Task<PagedResponse<MajorResponseDTO>> GetAllMajor(int pageNumber = 1, int pageSize = 8, string? search = null)
        {
            var major = await _majorRepository.GetAllMajorAsync();

            if (!string.IsNullOrWhiteSpace(search))
            {
                major = major
                    .Where(c => c.MajorName != null &&
                                c.MajorName.Contains(search))
                    .ToList();
            }

            var totalCount = major.Count;

            var majors = major
                .OrderBy(c => c.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var majorsDto = _mapper.Map<List<MajorResponseDTO>>(majors);

            return new PagedResponse<MajorResponseDTO>
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize),
                Data = majorsDto
            };
        }
        //Lấy khoa theo Id
        public async Task<MajorResponseDTO?> GetMajorById(int Id)
        {
            var Major = await _majorRepository.GetMajorByIdAsync(Id);

            if (Major == null) return null;

            var dto = _mapper.Map<MajorResponseDTO>(Major);

            return dto;
        }


        //Thêm khoa mới
        public async Task<MajorResponseDTO> CreateMajor(MajorRequestDTO dto)
        {
            var Major = _mapper.Map<Major>(dto);
            var MajorAdd = await _majorRepository.AddMajorAsync(Major);
            if (MajorAdd is null)
            {
                return null;
            }
            return _mapper.Map<MajorResponseDTO>(MajorAdd);
        }

        // Cập nhật giảng viên
        public async Task<MajorResponseDTO?> UpdateMajor(int id, MajorRequestDTO dto)
        {
            var MajorEntity = await _majorRepository.GetMajorByIdAsync(id);
            if (MajorEntity == null) return null;

            _mapper.Map(dto, MajorEntity);

            await _majorRepository.UpdateMajorAsync(MajorEntity);

            var updatedMajor = await _majorRepository.GetMajorByIdAsync(id);

            return _mapper.Map<MajorResponseDTO>(updatedMajor);
        }

        // Xóa tài khoản
        public async Task<bool> DeleteMajor(int id)
        {
            var MajorEntity = await _majorRepository.GetMajorByIdAsync(id);
            if (MajorEntity == null) return false;

            await _majorRepository.DeleteMajorAsync(MajorEntity);
            return true;
        }
    }
}
