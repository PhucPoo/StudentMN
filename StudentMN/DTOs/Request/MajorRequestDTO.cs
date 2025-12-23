using System.ComponentModel.DataAnnotations;

namespace StudentMN.DTOs.Request
{
    public class MajorRequestDTO
    {
        [Required(ErrorMessage = "MajorName cannot be empty.")]
        public string? MajorName { get; set; }
        [Required(ErrorMessage = "Description cannot be empty.")]
        public string? Description { get; set; }
    }
}
