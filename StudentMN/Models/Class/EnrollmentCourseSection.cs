using StudentMN.Models.Account;

namespace StudentMN.Models.Class
{
    public class EnrollmentCourseSection
    {
        public int StudentId { get; set; }
        public Student Student { get; set; } = null!;

        public int CourseSectionId { get; set; }
        public CourseSection CourseSection { get; set; } = null!;

        public DateTime RegisteredAt { get; set; } = DateTime.Now;
    }
}
