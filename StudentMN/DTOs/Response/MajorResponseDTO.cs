using StudentMN.Models.Base;

namespace StudentMN.DTOs.Response
{
    public class MajorResponseDTO:BaseEntity
    {
        public string? MajorName { get; set; }
        public string? Description { get; set; }
    }
}
