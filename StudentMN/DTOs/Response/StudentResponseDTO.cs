using StudentMN.Models;

namespace StudentMN.DTOs.Response
{
    public class StudentResponseDTO: BaseEntity
    {
        public string Avt { get; set; }
        public string StudentCode { get; set; }
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        
    }
}
