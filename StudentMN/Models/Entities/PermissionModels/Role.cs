using StudentMN.Models.Base;
using StudentMN.Models.Entities.Account;

namespace StudentMN.Models.Entities.PermissionModels
{
    public class Role:BaseEntity
    {
        public string? RoleName { get; set; }
        public string? Description { get; set; }

        public ICollection<User>? Users { get; set; }
        public ICollection<RolePermission>? RolePermissions { get; set; }
    }
}
