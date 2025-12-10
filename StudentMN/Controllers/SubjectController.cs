using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentMN.DTOs.Request;
using StudentMN.DTOs.Response;
using StudentMN.Services;

namespace StudentMN.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubjectController : ControllerBase
    {
        private readonly SubjectService _service;

        public SubjectController(SubjectService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<SubjectResponseDTO>>> GetAll(int pageNumber = 1, int pageSize = 8, string search = null)
        {
            return Ok(await _service.GetAllAsync(pageNumber, pageSize, search));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<SubjectResponseDTO>> Create(SubjectRequestDTO dto)
        {
            var Subject = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetAll), new { id = Subject.Id }, Subject);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult<SubjectResponseDTO>> Update(int id, SubjectRequestDTO dto)
        {
            var Subject = await _service.UpdateAsync(id, dto);
            if (Subject == null) return NotFound();
            return Ok(Subject);
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
