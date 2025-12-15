using System.ComponentModel.DataAnnotations;

namespace StudentMN.DTOs.Request
{
    public class SubjectRequestDTO
    {
        [Required]
        public int SubjectCode { get; set; }
        [Required]
        public string? SubjectName { get; set; }
        public int Credits { get; set; }
        public int MajorId { get; set; }
    }
}
