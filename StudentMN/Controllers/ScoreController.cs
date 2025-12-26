using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentMN.DTOs.Request;
using StudentMN.DTOs.Response;
using StudentMN.Services.Interfaces;


namespace ScoreMN.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ScoreController : ControllerBase
    {
        private readonly IScoreService _service;

        public ScoreController(IScoreService service)
        {
            _service = service;
        }

        // Lấy danh sách điểm với paging
        //[Authorize]
        [HttpGet]
        public async Task<ActionResult<PagedResponse<ScoreResponseDTO>>> GetAllScore(
            int pageNumber = 1, int pageSize = 8, string? search = null)
        {
            var scores = await _service.GetAllScore(pageNumber, pageSize, search);
            return Ok(scores);
        }

        // Lấy điểm của 1 Score theo ScoreId
        //[Authorize]
        [HttpGet("Score/{ScoreId}")]
        public async Task<ActionResult<List<ScoreResponseDTO>>> GetScoresByStudent(int ScoreId)
        {
            var scores = await _service.GetScoresByStudent(ScoreId);
            return Ok(scores);
        }
        [HttpPost]
        public async Task<ActionResult<ScoreResponseDTO>> CreateScore(ScoreRequestDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.ToDictionary(
                        kv => kv.Key,
                        kv => kv.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                    );
                    return BadRequest(new { success = false, errors });
                }
                var Score = await _service.AddScore(dto);
                if (Score is null)
                {
                    return BadRequest("");
                }
                return CreatedAtAction(nameof(GetAllScore), new { id = Score.Id }, Score);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = "System error",
                    detail = ex.InnerException?.Message ?? ex.Message
                });
            }
        }
        // Cập nhật điểm theo scoreId
        //[Authorize(Roles = "Admin")]
        [HttpPut("{ScoreId}/{subjectId}")]
        public async Task<ActionResult<ScoreResponseDTO>> UpdateScore(int ScoreId, int courseSectionId, ScoreRequestDTO dto)
        {
            var updatedScore = await _service.UpdateScore(ScoreId, courseSectionId, dto);
            if (updatedScore == null) return NotFound();
            return Ok(updatedScore);
        }
    }
}
