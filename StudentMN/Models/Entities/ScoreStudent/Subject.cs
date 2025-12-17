using StudentMN.Models.Base;
using StudentMN.Models.Entities.Class;

namespace StudentMN.Models.Entities.ScoreStudent
{
    public class Subject: BaseEntity
    {
        public int SubjectCode { get; set; }
        public string SubjectName { get; set; }
        public int Credits { get; set; }
        public int MajorId { get; set; }

        public Major Major { get; set; }
        public ICollection<CourseSection> CourseSections { get; set; }
        = new List<CourseSection>();
    }
}
