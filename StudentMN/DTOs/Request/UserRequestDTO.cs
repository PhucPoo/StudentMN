using System.ComponentModel.DataAnnotations;

namespace StudentMN.DTOs.Request
{
    public class UserRequestDTO
    {
        [Required(ErrorMessage = "Username cannot be empty")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password cannot be empty")]
        public string Password { get; set; }
        [Required(ErrorMessage = "FullName cannot be empty")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Email cannot be empty")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }
        [Required]
        public int RoleId { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
