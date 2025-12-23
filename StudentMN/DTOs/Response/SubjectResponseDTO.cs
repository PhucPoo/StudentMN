using StudentMN.Models.Base;

namespace StudentMN.DTOs.Response
{
    public class SubjectResponseDTO:BaseEntity
    {
        public string SubjectCode { get; set; }
        public string? SubjectName { get; set; }
        public int Credits { get; set; }
        public int MajorId { get; set; }
        public MajorResponseDTO Major { get; set; } = null!;
    }
}
