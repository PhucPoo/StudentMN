namespace StudentMN.Models
{
    public class Role:BaseEntity
    {
        public string RoleName { get; set; }
        public string Description { get; set; }

        public ICollection<User> Users { get; set; }
        public ICollection<RolePermission> RolePermissions { get; set; }
    }
}
