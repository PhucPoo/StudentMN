using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentMN.DTOs.Request;
using StudentMN.DTOs.Response;
using StudentMN.Services.Interfaces;

namespace StudentMN.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectService _service;

        public SubjectController(ISubjectService service)
        {
            _service = service;
        }

        //[Authorize]
        [HttpGet]
        public async Task<ActionResult<List<SubjectResponseDTO>>> GetAllSubject(int pageNumber = 1, int pageSize = 8, string? search = null)
        {
            return Ok(await _service.GetAllSubject(pageNumber, pageSize, search));
        }

        

        //[Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<SubjectResponseDTO>> CreateSubject(SubjectRequestDTO dto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.ToDictionary(
                    kv => kv.Key,
                    kv => kv.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                );
                return BadRequest(new { success = false, errors });
            }
            var Subject = await _service.CreateSubject(dto);
            return CreatedAtAction(nameof(GetAllSubject), new { id = Subject.Id }, Subject);
        }

        //[Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult<SubjectResponseDTO>> UpdateSubject(int id, SubjectRequestDTO dto)
        {
            var Subject = await _service.UpdateSubject(id, dto);
            if (Subject == null) return NotFound("subject not found");
            return Ok(Subject);
        }

        //[Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSubject(int id)
        {
            var success = await _service.DeleteSubject(id);
            if (!success) return NotFound("subject not found");
            return NoContent();
        }
    }
}
