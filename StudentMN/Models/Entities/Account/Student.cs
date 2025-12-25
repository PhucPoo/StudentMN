using StudentMN.Models.Base;
using StudentMN.Models.Entities.Class;
using System.ComponentModel.DataAnnotations;

namespace StudentMN.Models.Entities.Account
{
    public class Student :BaseEntity
    {
        [Required(ErrorMessage = "Ảnh đại diện không được để trống không được để trống")]
        public string? Avt { get; set; }
        public string? StudentCode { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public string? Course { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public int UserId { get; set; }
        public int? ClassId { get; set; }

        // Navigation property
        public User? User { get; set; }
        public Classes? Class { get; set; }
        public ICollection<EnrollmentCourseSection> EnrollmentCourses { get; set; } = new List<EnrollmentCourseSection>();
    }
}
