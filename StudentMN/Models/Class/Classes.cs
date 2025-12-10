using StudentMN.Models.Account;
using StudentMN.Models.Base;

namespace StudentMN.Models.Class
{
    public class Classes:BaseEntity
    {
        public string ClassName { get; set; }
        public int CourseYear { get; set; }
        public int MajorId { get; set; }
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public Major Major { get; set; }

        public ICollection<Student> Students { get; set; }
    }
}
