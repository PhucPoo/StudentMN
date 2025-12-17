using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
                _logger.LogError("Yeu cau khong hop le");
                return BadRequest();
            }
            var success = await _rolePermissionService.UpdateRolePermissionsAsync(request);

            if (!success)
            {
                _logger.LogError("Them quyen cho role khong thanh cong");
                return BadRequest();
            }

            _logger.LogInformation("quyen duoc them thanh cong");
            return Ok();
        }
        [HttpDelete("delete-role-permissions")]
        public async Task<IActionResult> RemoveRolePermissions([FromQuery] int roleId ,[FromQuery] int permissionId)
        {
            var result = await _rolePermissionService
                .RemovePermissionFromRoleAsync(roleId, permissionId);

            if (!result)
            {
                _logger.LogWarning("quyen hoac role khong ton tai");
                return NotFound();
            }

            _logger.LogInformation("Quyen duoc them thanh cong vao role: {roleId}",roleId);
            return Ok();
        }
    }
}
