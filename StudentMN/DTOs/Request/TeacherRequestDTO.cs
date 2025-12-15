using System.ComponentModel.DataAnnotations;

namespace StudentMN.DTOs.Request
{
    public class TeacherRequestDTO
    {
        [Required(ErrorMessage = "Avatar không được để trống.")]
        public string? Avt { get; set; }

        [Required(ErrorMessage = "TeacherCode không được để trống.")]
        public string? TeacherCode { get; set; }

        [Required(ErrorMessage = "FullName không được để trống.")]
        public string? FullName { get; set; }

        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Gender không được để trống.")]
        [RegularExpression("Male|Female|Other", ErrorMessage = "Gender phải là Male, Female hoặc Other.")]
        public string? Gender { get; set; }

        [Required(ErrorMessage = "Email không được để trống.")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "PhoneNumber không được để trống.")]
        [Phone(ErrorMessage = "Số điện thoại không hợp lệ.")]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Address không được để trống.")]
        public string? Address { get; set; }
        public int UserId { get; set; }
        public int MajorId { get; set; }
    }
}
