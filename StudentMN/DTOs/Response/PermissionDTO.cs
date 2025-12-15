namespace StudentMN.DTOs.Response
{
    public class PermissionDTO
    {
        public int Id { get; set; }
        public string PermissionName { get; set; }
        public string Description { get; set; }
        public bool IsAssigned { get; set; }
    }
}
