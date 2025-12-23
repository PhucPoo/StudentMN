using System.ComponentModel.DataAnnotations;

namespace StudentMN.DTOs.Request
{
    public class LoginRequest

    {
        [Required(ErrorMessage = "Username cannot be empty.")]
        public string? Username { get; set; }
        [Required(ErrorMessage = "Password cannot be empty.")]
        public string? Password { get; set; }
        
    }
}
