using StudentMN.Models.Account;

namespace StudentMN.Models.Permission
{
    public class RolePermission: BaseEntity
    {
        public int RoleId { get; set; }
        public int PermissionId { get; set; }

        public Role Role { get; set; }
        public Permission Permission { get; set; }
    }
}
