using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentMN.DTOs.Request;
using StudentMN.DTOs.Response;
using StudentMN.Services.Interfaces;

namespace StudentMN.Controllers
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

        //[Authorize]
        [HttpGet]
        public async Task<ActionResult<List<ScoreResponseDTO>>> GetAllScore(int pageNumber = 1, int pageSize = 8, string? search = null)
        {
            return Ok(await _service.GetAllScore(pageNumber, pageSize, search));
        }

       
        //[Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult<ScoreResponseDTO>> UpdateScore(int id, ScoreRequestDTO dto)
        {
            var Score = await _service.UpdateScore(id, dto);
            if (Score == null) return NotFound();
            return Ok(Score);
        }
    } 
}
    
