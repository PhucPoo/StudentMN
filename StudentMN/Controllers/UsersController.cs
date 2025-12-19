
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentMN.DTOs.Request;
using StudentMN.DTOs.Response;
using StudentMN.Services;
using StudentMN.Services.Interfaces;

namespace StudentManagement.StudentManagement.API.Controllers
{
    //[Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        //[Authorize]
        [HttpGet]
        public async Task<ActionResult<List<UserResponseDTO>>> GetAllUser(int pageNumber = 1, int pageSize = 8, string? search = null)
        {
            return Ok(await _userService.GetAllUser(pageNumber, pageSize, search));
        }

        //[Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<UserResponseDTO>> CreateUser(UserRequestDTO dto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.ToDictionary(
                    kv => kv.Key,
                    kv => kv.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                );
                return BadRequest(new { success = false, errors });
            }
            var user = await _userService.CreateUser(dto);
                return CreatedAtAction(nameof(GetAllUser), new { id = user.Id }, user);
            }

            //[Authorize(Roles = "Admin")]
            [HttpPut("{id}")]
            public async Task<ActionResult<UserResponseDTO>> UpdateUser(int id, UserRequestDTO dto)
            {
                var user = await _userService.UpdateUser(id, dto);
                if (user == null) return NotFound();
                return Ok(user);
            }

            //[Authorize(Roles = "Admin")]
            [HttpDelete("{id}")]
            public async Task<ActionResult> DeleteUser(int id)
            {
                var success = await _userService.DeleteUser(id);
                if (!success) return NotFound();
                return NoContent();
            }
        }


    }


