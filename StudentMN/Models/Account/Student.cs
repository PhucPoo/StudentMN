using StudentMN.Models.Base;
using StudentMN.Models.Class;
using System.ComponentModel.DataAnnotations;

namespace StudentMN.Models.Account
{
    public class Student :BaseEntity
    {
        [Required(ErrorMessage = "Ảnh đại diện không được để trống không được để trống")]
        public string? Avt { get; set; }
        public string? StudentCode { get; set; }
        public string? FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public int UserId { get; set; }
        public int? ClassId { get; set; }

        // Navigation property
        public User? User { get; set; }
        public Classes? Class { get; set; }
        public ICollection<EnrollmentCourseSection> EnrollmentCourseSections { get; set; }
        = new List<EnrollmentCourseSection>();
    }
}
