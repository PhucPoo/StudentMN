using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentMN.DTOs.Request;
using StudentMN.DTOs.Response;
using StudentMN.Services;

namespace StudentMN.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ScoreController : ControllerBase
    {
        private readonly ScoreService _service;

        public ScoreController(ScoreService service)
        {
            _service = service;
        }

        //[Authorize]
        [HttpGet]
        public async Task<ActionResult<List<ScoreResponseDTO>>> GetAllScore(int pageNumber = 1, int pageSize = 8, string? search = null)
        {
            return Ok(await _service.GetAllScoreAsync(pageNumber, pageSize, search));
        }

        //[Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<ScoreResponseDTO>> CreateScore(ScoreRequestDTO dto)
        {
            var Score = await _service.CreateScoreAsync(dto);
            return CreatedAtAction(nameof(GetAllScore), new { id = Score.Id }, Score);
        }

        //[Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult<ScoreResponseDTO>> UpdateScore(int id, ScoreRequestDTO dto)
        {
            var Score = await _service.UpdateScoreAsync(id, dto);
            if (Score == null) return NotFound();
            return Ok(Score);
        }
    } 
}
    
