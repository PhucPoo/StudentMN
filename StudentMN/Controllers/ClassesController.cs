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

            [Authorize]
            [HttpGet]
            public async Task<ActionResult<List<ClassesResponseDTO>>> GetAll(int pageNumber = 1, int pageSize = 8, string search = null)
            {
                return Ok(await _service.GetAllAsync(pageNumber, pageSize, search));
            }

            [Authorize(Roles = "Admin")]
            [HttpPost]
            public async Task<ActionResult<ClassesResponseDTO>> Create(ClassesRequestDTO dto)
            {
                var Class = await _service.CreateAsync(dto);
                return CreatedAtAction(nameof(GetAll), new { id = Class.Id }, Class);
            }

            [Authorize(Roles = "Admin")]
            [HttpPut("{id}")]
            public async Task<ActionResult<ClassesResponseDTO>> Update(int id, ClassesRequestDTO dto)
            {
                var Class = await _service.UpdateAsync(id, dto);
                if (Class == null) return NotFound();
                return Ok(Class);
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

}
