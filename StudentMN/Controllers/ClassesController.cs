
using StudentMN.DTOs.Request;
using StudentMN.DTOs.Response;
using Microsoft.AspNetCore.Mvc;
using ClassMN.Services.Interfaces;


namespace StudentMN.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClassesController : ControllerBase
    {
        private readonly ICLassService _service;

        public ClassesController(ICLassService service)
        {
            _service = service;
        }

        //[Authorize]
        [HttpGet]
        public async Task<ActionResult<List<ClassesResponseDTO>>> GetAllClass(int pageNumber = 1, int pageSize = 8, string? search = null)
        {
            return Ok(await _service.GetAllClass(pageNumber, pageSize, search));
        }

        //[Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<ClassesResponseDTO>> CreateClass(ClassesRequestDTO dto)
        {
            var Class = await _service.CreateClass(dto);
            return CreatedAtAction(nameof(GetAllClass), new { id = Class.Id }, Class);
        }

        //[Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult<ClassesResponseDTO>> UpdateClass(int id, ClassesRequestDTO dto)
        {
            var Class = await _service.UpdateClass(id, dto);
            if (Class == null) return NotFound("unable to get class");
            return Ok(Class);
        }

        //[Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteClass(int id)
        {
            var success = await _service.DeleteClass(id);
            if (!success) return NotFound("unable to get class");
            return NoContent();
        }
    }
}


