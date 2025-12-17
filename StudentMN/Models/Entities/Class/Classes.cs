using StudentMN.Models.Base;
using StudentMN.Models.Entities.Account;

namespace StudentMN.Models.Entities.Class
{
    public class Classes:BaseEntity
    {
        public string? ClassName { get; set; }
        public string? CourseYear { get; set; }
        public int MajorId { get; set; }
        public int TeacherId { get; set; }
        public Teacher? Teacher { get; set; }
        public Major? Major { get; set; }
        public ICollection<Student>? Students { get; set; }
    }
}
