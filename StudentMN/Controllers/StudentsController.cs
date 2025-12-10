
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentMN.DTOs.Request;
using StudentMN.DTOs.Response;
using StudentMN.Services;

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

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<List<StudentResponseDTO>>> GetAll(int pageNumber = 1, int pageSize = 8, string search = null)
        {
            return Ok(await _service.GetAllAsync(pageNumber,pageSize,search));
        }

        [HttpGet("by-user/{userId}")]
        public async Task<IActionResult> GetByUserId(int userId)
        {
            var student = await _service.GetByUserIdAsync(userId);

            if (student == null)
                return NotFound(new { success = false, message = "Không tìm thấy sinh viên" });

            return Ok(new { success = true, data = student });
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<StudentResponseDTO>> Create(StudentRequestDTO dto)
        {
            var student = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetAll), new { id = student.Id }, student);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<StudentResponseDTO>> Update(int id, StudentRequestDTO dto)
        {
            var student = await _service.UpdateAsync(id, dto);
            if (student == null) return NotFound();
            return Ok(student);
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


