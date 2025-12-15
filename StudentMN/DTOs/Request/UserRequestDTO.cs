using System.ComponentModel.DataAnnotations;

namespace StudentMN.DTOs.Request
{
    public class UserRequestDTO
    {
        [Required(ErrorMessage = "Tên không được để trống")]
        public string Username { get; set; }
        [Required]

        public string Password { get; set; }
        [Required(ErrorMessage = "FullName không được để trống")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Email không được để trống")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; }
        [Required]
        public int RoleId { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
