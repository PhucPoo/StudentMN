using StudentMN.Models;

namespace StudentMN.DTOs.Response
{
    public class UserResponseDTO:BaseEntity
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
