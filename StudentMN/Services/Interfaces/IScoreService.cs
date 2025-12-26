using StudentMN.DTOs.Request;
using StudentMN.DTOs.Response;

namespace StudentMN.Services.Interfaces
{
    public interface IScoreService
    {
        Task<PagedResponse<ScoreResponseDTO>> GetAllScore(int pageNumber = 1, int pageSize = 8, string? search = null);
        Task<ScoreResponseDTO> AddScore(ScoreRequestDTO dto);
        Task<ScoreResponseDTO?> GetScoresByStudent(int studentId);
        Task<ScoreResponseDTO?> UpdateScore(int studentId, int courseSectionId, ScoreRequestDTO dto);

    }
}
