using StudentMN.Models.Base;

namespace StudentMN.DTOs.Response
{
    public class ClassesResponseDTO:BaseEntity
    {
        public string ClassName { get; set; }
        public string CourseYear { get; set; }
        public int MajorId { get; set; }
        public int TeacherId { get; set; }
    }
}
