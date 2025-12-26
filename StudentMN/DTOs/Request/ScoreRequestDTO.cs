namespace StudentMN.DTOs.Request
{
    public class ScoreRequestDTO
    {
        public int StudentId { get; set; }
        public int CourseSectionId { get; set; }

        public float? AttendanceScore { get; set; }
        public float? MidtermScore { get; set; }
        public float? FinalScore { get; set; }

    }
}
