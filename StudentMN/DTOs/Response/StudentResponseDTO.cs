using StudentMN.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace StudentMN.DTOs.Response
{
    public class StudentResponseDTO: BaseEntity
    {
        public string? Avt { get; set; }
        public string? StudentCode { get; set; }

        [Required(ErrorMessage = "Ngày sinh không được để trống")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public string? Course { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public int UserId { get; set; }
        public UserResponseDTO User { get; set; } = null!;
        public int? ClassId { get; set; }
        public ClassesResponseDTO? Class { get; set; }

    }
}
