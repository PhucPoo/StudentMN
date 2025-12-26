using StudentMN.Models.Base;
using StudentMN.Models.Entities.Account;
using StudentMN.Models.Entities.Class;

namespace StudentMN.Models.Entities.ScoreStudent
{
    public class Score:BaseEntity
    {
        public int StudentId { get; set; }

        public float? AttendanceScore { get; set; }
        public float? MidtermScore { get; set; }
        public float? FinalScore { get; set; }
        public float? AverageScore { get; set; }

        public Student Student { get; set; }
        public int CourseSectionId { get; set; }
        public CourseSection CourseSection { get; set; } = null!;

    }
}
