using StudentMN.Models.Entities.Account;

namespace StudentMN.Models.Entities.PermissionModels
{
    public class UserRole
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}
