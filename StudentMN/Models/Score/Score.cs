using StudentMN.Models.Account;
using StudentMN.Models.Base;

namespace StudentMN.Models.Score
{
    public class Score:BaseEntity
    {
        public int StudentId { get; set; }

        public int SubjectId { get; set; }
        public float? AttendanceScore { get; set; }
        public float? MidtermScore { get; set; }
        public float? FinalScore { get; set; }
        public float? AverageScore { get; set; }

        public Student Student { get; set; }
        public Subject Subject { get; set; }

    }
}
