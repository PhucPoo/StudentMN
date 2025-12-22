using StudentMN.Models.Entities.Account;
using StudentMN.Models.Entities.ScoreStudent;

namespace StudentMN.DTOs.Request
{
    public class CourseSectionRequestDTO
    {
        public string SectionCode { get; set; } = null!;

        public int SubjectId { get; set; }

        public int TeacherId { get; set; }

        public string Semester { get; set; } = null!;

        public int MaxStudents { get; set; }
    }
}
