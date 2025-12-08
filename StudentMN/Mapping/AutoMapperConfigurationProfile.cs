using AutoMapper;
using StudentMN.DTOs.Request;
using StudentMN.DTOs.Response;
using StudentMN.Models;
using StudentMN.Models.Account;

namespace StudentMN.Mapping
{
    public class AutoMapperConfigurationProfile : Profile
    {
        public AutoMapperConfigurationProfile()
        {
            CreateMap<User, UserResponseDTO>().ReverseMap();
            CreateMap<UserRequestDTO, User>();
            CreateMap<Student, StudentResponseDTO>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.User.FullName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email));
            CreateMap<StudentRequestDTO, Student>();
        }
    }

}
