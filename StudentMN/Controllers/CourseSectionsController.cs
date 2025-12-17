using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentMN.DTOs.Request;
using StudentMN.DTOs.Response;
using StudentMN.Models.Entities.Class;
using StudentMN.Services;

namespace StudentMN.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourseSectionsController : ControllerBase
    {
        private readonly CourseSectionService _service;

        public CourseSectionsController(CourseSectionService service)
        {
            _service = service;
        }

        //[Authorize]
        [HttpGet]
        public async Task<ActionResult<List<CourseSectionResponseDTO>>> GetAllCourseSection(int pageNumber = 1, int pageSize = 8, string? search = null)
        {
            return Ok(await _service.GetAllCourseSectionAsync(pageNumber, pageSize, search));
        }

        //[Authorize]
        [HttpPost]
        public async Task<ActionResult<CourseSectionResponseDTO>> CreateCourseSection(CourseSectionRequestDTO dto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.ToDictionary(
                    kv => kv.Key,
                    kv => kv.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                );
                return BadRequest(new { success = false, errors });
            }
            var courseSection = await _service.CreateCourseSectionAsync(dto);
            return CreatedAtAction(nameof(GetAllCourseSection), new { id = courseSection.Id }, courseSection);
        }

        //[Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult<CourseSectionResponseDTO>> UpdateCourseSection(int id, CourseSectionRequestDTO dto)
        {
            var courseSection = await _service.UpdateCourseSectionAsync(id, dto);
            if (courseSection == null) return NotFound();
            return Ok(courseSection);
        }

        //[Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCourseSection(int id)
        {
            var success = await _service.DeleteCourseSectionAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }


}
