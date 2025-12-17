using StudentMN.Models.Base;
using StudentMN.Models.Entities.Account;
using StudentMN.Models.Entities.Class;

namespace StudentMN.DTOs.Response
{
    public class EnrollmentResponseDTO:BaseEntity
    {
        public int StudentId { get; set; }
        public Student Student { get; set; } = null!;

        public int CourseSectionId { get; set; }
        public CourseSection CourseSection { get; set; } = null!;

    }
}
