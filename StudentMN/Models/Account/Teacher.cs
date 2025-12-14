using StudentMN.Models.Base;
using StudentMN.Models.Class;

namespace StudentMN.Models.Account
{
    public class Teacher: BaseEntity
    {
        public string? Avt { get; set; }
        public string? TeacherCode { get; set; }
        public string? FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public int MajorId { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }

        public Major? Major { get; set; }


    }
}
