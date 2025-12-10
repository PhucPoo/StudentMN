namespace StudentMN.DTOs.Request
{
    public class SubjectRequestDTO
    {
        public int SubjectCode { get; set; }
        public string SubjectName { get; set; }
        public int Credits { get; set; }
        public int MajorId { get; set; }
    }
}
