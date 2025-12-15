using StudentMN.Models.Base;
using StudentMN.Models.PermissionModels;

namespace StudentMN.Models.Account
{
    public class Role:BaseEntity
    {
        public string? RoleName { get; set; }
        public string? Description { get; set; }

        public ICollection<User>? Users { get; set; }
        public ICollection<RolePermission>? RolePermissions { get; set; }
    }
}
