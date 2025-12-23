using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentMN.DTOs.Request;
using StudentMN.DTOs.Response;
using StudentMN.Models.Entities.PermissionModels;
using StudentMN.Services.Interfaces;

namespace StudentMN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionsController : ControllerBase
    {
        private readonly IPermissionService _permissionService;

        public PermissionsController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        [HttpGet]
        public async Task<ActionResult<List<PermissionDTO>>> GetAllPermission(int pageNumber = 1, int pageSize = 8, string? search = null)
        {
            return Ok(await _permissionService.GetAllPermissionAsync(pageNumber, pageSize, search));
        }

        [HttpPost]
        public async Task<IActionResult> CreatePermission(PermissionRequestDTO permission)
        {
            var created = await _permissionService.CreatePermissionAsync(permission);
            return Ok(created);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePermission(int id, Permission permission)
        {
            var updated = await _permissionService.UpdatePermissionAsync(id, permission);
            if (updated == null)
                return NotFound("unable to get student");
            return Ok(updated);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePermission(int id)
        {
            var success = await _permissionService.DeletePermissionAsync(id);
            if (!success)
                return NotFound("unable to get student");
            return NoContent();
        }
    }
}
