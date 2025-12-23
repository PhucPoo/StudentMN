
using System.ComponentModel.DataAnnotations;

namespace StudentMN.DTOs.Request
{
    public class StudentRequestDTO
    {
        public string? Avt { get; set; }
        public string? StudentCode { get; set; }

        [Required(ErrorMessage = "Ngày sinh không được để trống")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }
        [Required(ErrorMessage = "Gender cannot be empty.")]
        public string? Gender { get; set; }
        [Required(ErrorMessage = "Course cannot be empty.")]
        [RegularExpression(@"^K\d{2}$", ErrorMessage = "Course must start with 'K' followed by exactly 2 digits.")]
        public string? Course { get; set; }
        [Required(ErrorMessage = "Phone number cannot be empty.")]
        [RegularExpression(@"^0\d{9,10}$", ErrorMessage = "Invalid phone number. It must start with 0 and be 10-11 digits long.")]
        public string? PhoneNumber { get; set; }
        [Required(ErrorMessage = "Address cannot be empty.")]
        public string? Address { get; set; }
        public int UserId { get; set; }
        public int? ClassId { get; set; }
    }
}
