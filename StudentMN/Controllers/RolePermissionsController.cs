using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentMN.DTOs.Request;
using StudentMN.Services.Interfaces;

namespace StudentMN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolePermissionsController : ControllerBase
    {
        private readonly IRolePermissionService _rolePermissionService;
        private readonly ILogger<RolePermissionsController> _logger;
        public RolePermissionsController(IRolePermissionService rolePermissionService, ILogger<RolePermissionsController> logger)
        {
            _rolePermissionService = rolePermissionService;
            _logger = logger;
        }

        [HttpGet("get-role-permissions")]
        public async Task<IActionResult> GetRolesWithPermissions()
        {
            var result = await _rolePermissionService.GetRolesWithPermissionsAsync();
            return Ok(result);
        }

        //[Authorize(Roles = "Admin")]  
        [HttpPost("add-role-permissions")]
        public async Task<IActionResult> UpdateRolePermissions([FromBody] RolePermissionRequestDTO request)
        {
            if (request == null || request.PermissionIds == null)
            {
                _logger.LogError("Invalid request");
                return BadRequest();
            }
            var success = await _rolePermissionService.UpdateRolePermissionsAsync(request);

            if (!success)
            {
                _logger.LogError("Failed to add permission to role");
                return BadRequest();
            }

            _logger.LogInformation("permission added successfully");
            return Ok();
        }
        [HttpDelete("delete-role-permissions")]
        public async Task<IActionResult> RemoveRolePermissions([FromQuery] int roleId ,[FromQuery] int permissionId)
        {
            var result = await _rolePermissionService
                .RemovePermissionFromRoleAsync(roleId, permissionId);

            if (!result)
            {
                _logger.LogWarning("permission or role does not exist");
                return NotFound();
            }

            _logger.LogInformation("Permission successfully added to role: {roleId}", roleId);
            return Ok();
        }
    }
}
