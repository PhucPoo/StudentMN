
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
        public string? Gender { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public int UserId { get; set; }
    }
}
