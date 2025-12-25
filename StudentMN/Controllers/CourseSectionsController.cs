using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentMN.DTOs.Request;
using StudentMN.DTOs.Response;
using StudentMN.Services.Interfaces;

namespace StudentMN.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourseSectionsController : ControllerBase
    {
        private readonly ICourseSectionService _service;

        public CourseSectionsController(ICourseSectionService service)
        {
            _service = service;
        }

        //[Authorize]
        [HttpGet]
        public async Task<ActionResult<List<CourseSectionResponseDTO>>> GetAllCourseSection(int pageNumber = 1, int pageSize = 8, string? search = null)
        {
            return Ok(await _service.GetAllCourseSection(pageNumber, pageSize, search));
        }

        //[Authorize]
        [HttpPost]
        public async Task<ActionResult<CourseSectionResponseDTO>> CreateCourseSection(CourseSectionRequestDTO dto)
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
                var courseSection = await _service.CreateCourseSection(dto);
                return CreatedAtAction(nameof(GetAllCourseSection), new { id = courseSection.Id }, courseSection);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = "System error",
                    detail = ex.InnerException?.Message ?? ex.Message
                });
            }
        }

        //[Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult<CourseSectionResponseDTO>> UpdateCourseSection(int id, CourseSectionRequestDTO dto)
        {
            var courseSection = await _service.UpdateCourseSection(id, dto);
            if (courseSection == null) return NotFound("unable to get courseSection");
            return Ok(courseSection);
        }

        //[Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCourseSection(int id)
        {
            var success = await _service.DeleteCourseSection(id);
            if (!success) return NotFound("unable to get courseSection");
            return NoContent();
        }
    }


}
