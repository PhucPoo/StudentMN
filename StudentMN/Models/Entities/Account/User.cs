using StudentMN.Models.Base;
using StudentMN.Models.Entities.PermissionModels;
using System.ComponentModel.DataAnnotations;

namespace StudentMN.Models.Entities.Account
{
    public class User: BaseEntity
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
        public virtual Role? Role { get; set; }

        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
    }
}
