namespace StudentMN.DTOs.Response
{
    public class RolePermissionDTO
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public List<PermissionDTO> Permissions { get; set; }
    }
}
