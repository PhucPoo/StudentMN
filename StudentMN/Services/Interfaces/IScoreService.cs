using StudentMN.DTOs.Request;
using StudentMN.DTOs.Response;

namespace StudentMN.Services.Interfaces
{
    public interface IScoreService
    {
        Task<PagedResponse<ScoreResponseDTO>> GetAllScore(int pageNumber = 1, int pageSize = 8, string? search = null);
        Task<ScoreResponseDTO?> GetScoreById(int id);

        Task<ScoreResponseDTO?> UpdateScore(int id, ScoreRequestDTO dto);

    }
}
