using System.ComponentModel.DataAnnotations;

namespace StudentMN.DTOs.Request
{
    public class MajorRequestDTO
    {
        [Required]
        public string? MajorName { get; set; }
        [Required]
        public string? Description { get; set; }
    }
}
