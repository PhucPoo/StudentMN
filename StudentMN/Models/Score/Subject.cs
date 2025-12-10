using StudentMN.Models.Base;
using StudentMN.Models.Class;

namespace StudentMN.Models.Score
{
    public class Subject: BaseEntity
    {
        public int SubjectCode { get; set; }
        public string SubjectName { get; set; }
        public int Credits { get; set; }
        public int MajorId { get; set; }

        public Major Major { get; set; }
    }
}
