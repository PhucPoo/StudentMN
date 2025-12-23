using StudentMN.Models.Entities.Account;
using StudentMN.Models.Entities.ScoreStudent;
using System.ComponentModel.DataAnnotations;

namespace StudentMN.DTOs.Request
{
    public class CourseSectionRequestDTO
    {
        [Required(ErrorMessage = "SectionCode cannot be empty.")]
        public string SectionCode { get; set; } = null!;

        public int SubjectId { get; set; }

        public int TeacherId { get; set; }
        [Required(ErrorMessage = "Semester cannot be empty.")]

        public string Semester { get; set; } = null!;
        [Required(ErrorMessage = "MaxStudents cannot be empty.")]
        [Range(0, 200, ErrorMessage = "MaxStudents must be between 0 and 200.")]

        public int MaxStudents { get; set; }
    }
}
