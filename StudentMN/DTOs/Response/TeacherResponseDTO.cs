using StudentMN.Models.Base;

namespace StudentMN.DTOs.Response
{
    public class TeacherResponseDTO:BaseEntity
    {
        public string? Avt { get; set; }
        public string? TeacherCode { get; set; }
        public string? FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public int UserId { get; set; }
        public int MajorId { get; set; }
        public UserResponseDTO User { get; set; } = null!;
        public MajorResponseDTO Major { get; set; } = null!;


    }
}
