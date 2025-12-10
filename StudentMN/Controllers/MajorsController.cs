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
        public async Task<ActionResult<List<MajorResponseDTO>>> GetAll(int pageNumber = 1, int pageSize = 8, string search = null)
        {
            return Ok(await _service.GetAllAsync(pageNumber, pageSize, search));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<MajorResponseDTO>> Create(MajorRequestDTO dto)
        {
            var Major = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetAll), new { id = Major.Id }, Major);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult<MajorResponseDTO>> Update(int id, MajorRequestDTO dto)
        {
            var Major = await _service.UpdateAsync(id, dto);
            if (Major == null) return NotFound();
            return Ok(Major);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
