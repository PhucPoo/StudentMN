using StudentMN.Models.Base;
using StudentMN.Models.Entities.Account;

namespace StudentMN.Models.Entities.Class
{
    public class EnrollmentCourseSection:BaseEntity
    {
        public int StudentId { get; set; }
        public Student Student { get; set; } = null!;

        public int CourseSectionId { get; set; }
        public CourseSection CourseSection { get; set; } = null!;

    }
}
