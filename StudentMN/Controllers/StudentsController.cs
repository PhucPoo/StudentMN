
using Microsoft.AspNetCore.Mvc;
using StudentMN.DTOs.Request;
using StudentMN.DTOs.Response;
using StudentMN.Services.Interfaces;
using System.Security.Claims;

namespace StudentManagement.StudentManagement.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _service;

        public StudentsController(IStudentService service)
        {
            _service = service;
        }

        //[Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<List<StudentResponseDTO>>> GetAllStudent(int pageNumber = 1, int pageSize = 8, string? search = null)
        {
            return Ok(await _service.GetAllStudent(pageNumber, pageSize, search));
        }
        [HttpGet("export/class/{classId}")]
        public async Task<IActionResult> ExportStudentsByClass(int classId)
        {
            var fileBytes = await _service.ExportStudentsByClassToExcel(classId);

            return File(
                fileBytes,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                $"students_class_{classId}.xlsx"
            );
        }

        [HttpGet("by-id/{Id}")]
        public async Task<IActionResult> GetStudentById(int Id)
        {
            var student = await _service.GetStudentByClass(Id);

            if (student == null)
                return NotFound(new { success = false, message = "Student not found" });

            return Ok(new { success = true, data = student });
        }

        //[Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<StudentResponseDTO>> CreateStudent(StudentRequestDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.ToDictionary(
                        kv => kv.Key,
                        kv => kv.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                    );
                    return BadRequest(new { success = false, errors });
                }
                var student = await _service.CreateStudent(dto);
                if (student is null)
                {
                    return BadRequest("");
                }
                return CreatedAtAction(nameof(GetAllStudent), new { id = student.Id }, student);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<StudentResponseDTO>> UpdateStudent(int id, StudentRequestDTO dto)
        {
            var role = User.FindFirst(ClaimTypes.Role)?.Value;

            if (string.IsNullOrEmpty(role))
                return Forbid();

            var student = await _service.UpdateStudent(id, dto);
            if (student == null) return BadRequest("unable to get student");
            return Ok(student);
        }

        //[Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteStudent(int id)
        {
            var success = await _service.DeleteStudent(id);
            if (!success)
            {
                return NotFound(new { message = "Student not found", studentId = id });
            }
            return NoContent();
        }
    }


}


