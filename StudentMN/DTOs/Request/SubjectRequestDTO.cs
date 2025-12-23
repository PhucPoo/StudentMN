using System.ComponentModel.DataAnnotations;

namespace StudentMN.DTOs.Request
{
    public class SubjectRequestDTO
    {
        [Required(ErrorMessage = "SubjectCode cannot be empty.")]
        public int SubjectCode { get; set; }
        [Required(ErrorMessage = "SubjectName cannot be empty.")]
        public string? SubjectName { get; set; }
        [Required(ErrorMessage = "Credits cannot be empty.")]
        public int Credits { get; set; }
        public int MajorId { get; set; }
    }
}
