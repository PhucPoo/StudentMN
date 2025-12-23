using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentMN.DTOs.Request;
using StudentMN.DTOs.Response;
using StudentMN.Services;
using StudentMN.Services.Interfaces;

namespace StudentMN.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MajorsController : ControllerBase
    {
        private readonly IMajorService _service;

        public MajorsController(IMajorService service)
        {
            _service = service;
        }

        //[Authorize]
        [HttpGet]
        public async Task<ActionResult<List<MajorResponseDTO>>> GetAllMajor(int pageNumber = 1, int pageSize = 8, string? search = null)
        {
            return Ok(await _service.GetAllMajor(pageNumber, pageSize, search));
        }

        //[Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<MajorResponseDTO>> CreateMajor(MajorRequestDTO dto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.ToDictionary(
                    kv => kv.Key,
                    kv => kv.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                );
                return BadRequest(new { success = false, errors });
            }
            var Major = await _service.CreateMajor(dto);
            return CreatedAtAction(nameof(GetAllMajor), new { id = Major.Id }, Major);
        }

        //[Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult<MajorResponseDTO>> UpdateMajor(int id, MajorRequestDTO dto)
        {
            var Major = await _service.UpdateMajor(id, dto);
            if (Major == null) return NotFound("unable to get major");
            return Ok(Major);
        }

        //[Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMajor(int id)
        {
            var success = await _service.DeleteMajor(id);
            if (!success) return NotFound("unable to get major");
            return NoContent();
        }
    }
}
