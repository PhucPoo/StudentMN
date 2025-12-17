using StudentMN.Models.Base;

namespace StudentMN.Models.Entities.Class
{
    public class Major:BaseEntity
    {
        public string MajorName { get; set; }
        public string Description { get; set; }

        public ICollection<Classes> Classes { get; set; }
    }
}
