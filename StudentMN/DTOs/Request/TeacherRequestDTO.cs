using System.ComponentModel.DataAnnotations;

namespace StudentMN.DTOs.Request
{
    public class TeacherRequestDTO
    {
        [Required(ErrorMessage = "Avatar cannot be empty")]
        public string? Avt { get; set; }

        [Required(ErrorMessage = "TeacherCode cannot be empty.")]
        public string? TeacherCode { get; set; }

        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Gender cannot be empty.")]
        public string? Gender { get; set; }

        [Required(ErrorMessage = "Phone number cannot be empty.")]
        [RegularExpression(@"^0\d{9,10}$", ErrorMessage = "Invalid phone number. It must start with 0 and be 10-11 digits long.")]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Address cannot be empty.")]
        public string? Address { get; set; }
        public int UserId { get; set; }
        public int MajorId { get; set; }
    }
}
