namespace StudentMN.Controllers
{
    using global::StudentMN.DTOs.Request;
    using global::StudentMN.DTOs.Response;
    using global::StudentMN.Services;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
  

    namespace StudentMN.Controllers
    {
        [ApiController]
        [Route("api/[controller]")]
        public class ClassesController : ControllerBase
        {
            private readonly ClassService _service;

            public ClassesController(ClassService service)
            {
                _service = service;
            }

            //[Authorize]
            [HttpGet]
            public async Task<ActionResult<List<ClassesResponseDTO>>> GetAllClass(int pageNumber = 1, int pageSize = 8, string? search = null)
            {
                return Ok(await _service.GetAllClassAsync(pageNumber, pageSize, search));
            }

            //[Authorize(Roles = "Admin")]
            [HttpPost]
            public async Task<ActionResult<ClassesResponseDTO>> CreateClass(ClassesRequestDTO dto)
            {
                var Class = await _service.CreateClassAsync(dto);
                return CreatedAtAction(nameof(GetAllClass), new { id = Class.Id }, Class);
            }

            //[Authorize(Roles = "Admin")]
            [HttpPut("{id}")]
            public async Task<ActionResult<ClassesResponseDTO>> UpdateClass(int id, ClassesRequestDTO dto)
            {
                var Class = await _service.UpdateClassAsync(id, dto);
                if (Class == null) return NotFound();
                return Ok(Class);
            }

            //[Authorize(Roles = "Admin")]
            [HttpDelete("{id}")]
            public async Task<ActionResult> DeleteClass(int id)
            {
                var success = await _service.DeleteClassAsync(id);
                if (!success) return NotFound();
                return NoContent();
            }
        }
    }

}
