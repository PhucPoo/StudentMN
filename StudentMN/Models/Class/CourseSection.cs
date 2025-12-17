using StudentMN.Models.Account;
using StudentMN.Models.Base;
using StudentMN.Models.ScoreStudent;

namespace StudentMN.Models.Class
{
    public class CourseSection: BaseEntity
    {

        public string SectionCode { get; set; } = null!;

        public int SubjectId { get; set; }
        public Subject Subject { get; set; } = null!;

        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; } = null!;

        public string Semester { get; set; } = null!;

        public int MaxStudents { get; set; }

        public ICollection<EnrollmentCourseSection> EnrollmentCourseSections { get; set; }
            = new List<EnrollmentCourseSection>();

        public ICollection<Score> Scores { get; set; }
            = new List<Score>();
    }
}
