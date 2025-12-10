using StudentMN.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace StudentMN.DTOs.Response
{
    public class UserResponseDTO:BaseEntity
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
