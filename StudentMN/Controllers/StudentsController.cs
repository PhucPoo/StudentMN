
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentMN.DTOs.Request;
using StudentMN.DTOs.Response;
using StudentMN.Services;
using System.Security.Claims;

namespace StudentManagement.StudentManagement.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly StudentService _service;

        public StudentsController(StudentService service)
        {
            _service = service;
        }

        //[Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<List<StudentResponseDTO>>> GetAllStudent(int pageNumber = 1, int pageSize = 8, string? search = null)
        {
            return Ok(await _service.GetAllStudentAsync(pageNumber,pageSize,search));
        }

        [HttpGet("by-user/{userId}")]
        public async Task<IActionResult> GetStudentByUserId(int userId)
        {
            var student = await _service.GetStudentByUserIdAsync(userId);

            if (student == null)
                return NotFound(new { success = false, message = "Không tìm thấy sinh viên" });

            return Ok(new { success = true, data = student });
        }

        //[Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<StudentResponseDTO>> CreateStudent(StudentRequestDTO dto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.ToDictionary(
                    kv => kv.Key,
                    kv => kv.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                );
                return BadRequest(new { success = false, errors });
            }
            var student = await _service.CreateStudentAsync(dto);
            return CreatedAtAction(nameof(GetAllStudent), new { id = student.Id }, student);
        }

        //[Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<StudentResponseDTO>> UpdateStudent(int id, StudentRequestDTO dto)
        {
            var role = User.FindFirst(ClaimTypes.Role)?.Value;

            if (string.IsNullOrEmpty(role))
                return Forbid();

            var student = await _service.UpdateStudentAsync(id, dto,role);
            if (student == null) return NotFound();
            return Ok(student);
        }

        //[Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteStudent(int id)
        {
            var success = await _service.DeleteStudentAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }


}


