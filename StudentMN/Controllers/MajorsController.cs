using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentMN.DTOs.Request;
using StudentMN.DTOs.Response;
using StudentMN.Services;

namespace StudentMN.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MajorsController : ControllerBase
    {
        private readonly MajorService _service;

        public MajorsController(MajorService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<MajorResponseDTO>>> GetAllMajor(int pageNumber = 1, int pageSize = 8, string? search = null)
        {
            return Ok(await _service.GetAllMajorAsync(pageNumber, pageSize, search));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<MajorResponseDTO>> CreateMajor(MajorRequestDTO dto)
        {
            var Major = await _service.CreateMajorAsync(dto);
            return CreatedAtAction(nameof(GetAllMajor), new { id = Major.Id }, Major);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult<MajorResponseDTO>> UpdateMajor(int id, MajorRequestDTO dto)
        {
            var Major = await _service.UpdateMajorAsync(id, dto);
            if (Major == null) return NotFound();
            return Ok(Major);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMajor(int id)
        {
            var success = await _service.DeleteMajorAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
