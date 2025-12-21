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
    public class TeachersController : ControllerBase
    {
        private readonly ITeacherService _service;

        public TeachersController(ITeacherService service)
        {
            _service = service;
        }

        //[Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<List<TeacherResponseDTO>>> GetAllTeacher(int pageNumber = 1, int pageSize = 8, string? search = null)
        {
            return Ok(await _service.GetAllTeacher(pageNumber, pageSize, search));
        }
        [HttpGet("by-user/{userId}")]
        public async Task<IActionResult> GetTeacherById(int userId)
        {
            var teacher = await _service.GetTeacherById(userId);

            if (teacher == null)
                return NotFound(new { success = false, message = "Teacher not found" });

            return Ok(new { success = true, data = teacher });
        }

        //[Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<TeacherResponseDTO>> CreateTeacher(TeacherRequestDTO dto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.ToDictionary(
                    kv => kv.Key,
                    kv => kv.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                );
                return BadRequest(new { success = false, errors });
            }
            var Teacher = await _service.CreateTeacher(dto);
            return CreatedAtAction(nameof(GetAllTeacher), new { id = Teacher.Id }, Teacher);
        }

        //[Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<TeacherResponseDTO>> UpdateTeacher(int id, TeacherRequestDTO dto)
        {
            var Teacher = await _service.UpdateTeacher(id, dto);
            if (Teacher == null) return NotFound();
            return Ok(Teacher);
        }

        //[Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTeacher(int id)
        {
            var success = await _service.DeleteTeacher(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }

}
