using StudentMN.Models.Base;
namespace StudentMN.DTOs.Response
{
    public class EnrollmentResponseDTO:BaseEntity
    {
        public int Id { get; set; }

        public int StudentId { get; set; }
        public string StudentName { get; set; } = null!;
        public string ClassName { get; set; } = null!;

        public int CourseSectionId { get; set; }
        public string SectionCode { get; set; } = null!;
        public string SubjectCode { get; set; } = null!;
        public string SubjectName { get; set; } = null!;
        public int Credits { get; set; }
        public int MaxStudent { get; set; }
        public int Remaining { get; set; }
    }
}
