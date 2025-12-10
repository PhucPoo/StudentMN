using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentMN.DTOs.Request;
using StudentMN.DTOs.Response;
using TeacherMN.Services;

namespace TeacherMN.Controllers
{


    [ApiController]
    [Route("api/[controller]")]
    public class TeachersController : ControllerBase
    {
        private readonly TeacherService _service;

        public TeachersController(TeacherService service)
        {
            _service = service;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<List<TeacherResponseDTO>>> GetAll(int pageNumber = 1, int pageSize = 8, string search = null)
        {
            return Ok(await _service.GetAllAsync(pageNumber, pageSize, search));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<TeacherResponseDTO>> Create(TeacherRequestDTO dto)
        {
            var Teacher = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetAll), new { id = Teacher.Id }, Teacher);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<TeacherResponseDTO>> Update(int id, TeacherRequestDTO dto)
        {
            var Teacher = await _service.UpdateAsync(id, dto);
            if (Teacher == null) return NotFound();
            return Ok(Teacher);
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
