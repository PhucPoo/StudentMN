using AutoMapper;
using StudentMN.DTOs.Request;
using StudentMN.DTOs.Response;
using StudentMN.Models;

namespace StudentMN.Mapping
{
    public class AutoMapperConfigurationProfile : Profile
    {
        public AutoMapperConfigurationProfile()
        {
            CreateMap<User, UserResponseDTO>().ReverseMap();
            CreateMap<UserRequestDTO, User>();
            CreateMap<Student, StudentResponseDTO>().ReverseMap();
            CreateMap<StudentRequestDTO, Student>();
        }
    }

}
