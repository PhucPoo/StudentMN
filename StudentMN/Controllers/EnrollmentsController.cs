using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentMN.DTOs.Request;
using StudentMN.DTOs.Response;
using StudentMN.Models.Entities.Class;
using StudentMN.Services;
using StudentMN.Services.Interfaces;

namespace StudentMN.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnrollmentsController : ControllerBase
    {
        private readonly IEnrollmentCourseSection _service;

        public EnrollmentsController(IEnrollmentCourseSection service)
        {
            _service = service;
        }

        //[Authorize]
        [HttpGet]
        public async Task<ActionResult<List<EnrollmentResponseDTO>>> GetAllEnrollment(int pageNumber = 1, int pageSize = 8, string? search = null)
        {
            return Ok(await _service.GetAllEnrollments(pageNumber, pageSize, search));
        }

        //[Authorize]
        [HttpPost]
        public async Task<ActionResult<EnrollmentResponseDTO>> CreateEnrollment(EnrollmentRequestDTO dto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.ToDictionary(
                    kv => kv.Key,
                    kv => kv.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                );
                return BadRequest(new { success = false, errors });
            }
            var enrollment = await _service.CreateEnrollment(dto);
            return CreatedAtAction(nameof(GetAllEnrollment), new { id = enrollment.Id }, enrollment);
        }


        //[Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEnrollment(int id)
        {
            var success = await _service.DeleteEnrollment(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }


}
