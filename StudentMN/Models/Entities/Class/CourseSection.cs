using StudentMN.Models.Base;
using StudentMN.Models.Entities.Account;
using StudentMN.Models.Entities.ScoreStudent;

namespace StudentMN.Models.Entities.Class
{
    public class CourseSection: BaseEntity
    {

        public string SectionCode { get; set; } = null!;
        public int GroupNumber { get; set; }

        public int SubjectId { get; set; }
        public Subject Subject { get; set; } = null!;

        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; } = null!;

        public string Semester { get; set; } = null!;

        public int MaxStudents { get; set; }
        public int Remainning { get; set; }
        public ICollection<EnrollmentCourseSection> EnrollmentCourses { get; set; } = new List<EnrollmentCourseSection>();

    }
}
