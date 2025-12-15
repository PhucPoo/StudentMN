using StudentMN.DTOs.Response;

namespace StudentMN.DTOs.Request
{
    public class RolePermissionRequestDTO
    {
        public int RoleId { get; set; }
        public List<int> PermissionIds { get; set; }
    }
}
