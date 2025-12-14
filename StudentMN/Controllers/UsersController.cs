
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentMN.DTOs.Request;
using StudentMN.DTOs.Response;
using StudentMN.Services;

namespace StudentManagement.StudentManagement.API.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserService _service;

        public UsersController(UserService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<UserResponseDTO>>> GetAllUser(int pageNumber=1, int pageSize=8, string? search=null)
        {
            return Ok(await _service.GetAllUserAsync(pageNumber,pageSize,search));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<UserResponseDTO>> CreateUser(UserRequestDTO dto)
        {
            var user = await _service.CreateUserAsync(dto);
            return CreatedAtAction(nameof(GetAllUser), new { id = user.Id }, user);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult<UserResponseDTO>> UpdateUser(int id, UserRequestDTO dto)
        {
            var user = await _service.UpdateUserAsync(id, dto);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            var success = await _service.DeleteUserAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }


}


