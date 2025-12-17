using StudentMN.Models.Base;

namespace StudentMN.DTOs.Response
{
    public class CourseSectionResponseDTO:BaseEntity
    {
        public string SectionCode { get; set; } = null!;
        public string Semester { get; set; } = null!;
        public int MaxStudents { get; set; }

        public SubjectResponseDTO Subject { get; set; } = null!;
        public TeacherResponseDTO Teacher { get; set; } = null!;
    }
}
