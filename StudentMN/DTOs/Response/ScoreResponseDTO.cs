using StudentMN.Models.Base;

namespace StudentMN.DTOs.Response
{
    public class ScoreResponseDTO:BaseEntity
    {
        public int StudentId { get; set; }
        public int CourseSectionId { get; set; }

        public float? AttendanceScore { get; set; }
        public float? MidtermScore { get; set; }
        public float? FinalScore { get; set; }
        public float? AverageScore { get; set; }
        public StudentResponseDTO Student { get; set; } = null!;
        public CourseSectionResponseDTO CourseSection { get; set; } = null!;

    }
}
