namespace StudentMN.Models.Base
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }
        public Boolean IsDelete { get; set; }= false;
    }
}
