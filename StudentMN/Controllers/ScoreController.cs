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

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<ScoreResponseDTO>>> GetAll(int pageNumber = 1, int pageSize = 8, string search = null)
        {
            return Ok(await _service.GetAllAsync(pageNumber, pageSize, search));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<ScoreResponseDTO>> Create(ScoreRequestDTO dto)
        {
            var Score = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetAll), new { id = Score.Id }, Score);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult<ScoreResponseDTO>> Update(int id, ScoreRequestDTO dto)
        {
            var Score = await _service.UpdateAsync(id, dto);
            if (Score == null) return NotFound();
            return Ok(Score);
        }
    } 
}
    
