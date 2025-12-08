namespace StudentMN.Models.Permission
{
    public class Permission : BaseEntity
    {
        public string PermissionName { get; set; }
        public string Description { get; set; }

        // Navigation property
        public ICollection<RolePermission> RolePermissions { get; set; }
    }
}
