using AutoMapper;
using StudentMN.DTOs.Request;
using StudentMN.DTOs.Response;
using StudentMN.Models.Account;
using StudentMN.Models.Class;
using StudentMN.Models.Score;

namespace StudentMN.Mapping
{
    public class AutoMapperConfigurationProfile : Profile
    {
        public AutoMapperConfigurationProfile()
        {
            CreateMap<User, UserResponseDTO>().ReverseMap();
            CreateMap<UserRequestDTO, User>();
            CreateMap<Student, StudentResponseDTO>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.User!.FullName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User!.Email));
            CreateMap<StudentRequestDTO, Student>();
            CreateMap<Teacher, TeacherResponseDTO>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.User!.FullName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User!.Email));
            CreateMap<TeacherRequestDTO, Teacher>();
            CreateMap<MajorRequestDTO, Major>();
            CreateMap<Major, MajorResponseDTO>();
            CreateMap<ClassesRequestDTO, Classes>();
            CreateMap<Classes, ClassesResponseDTO>();
            CreateMap<SubjectRequestDTO, Subject>();
            CreateMap<Subject, SubjectResponseDTO>();
            CreateMap<ScoreRequestDTO, Score>();
            CreateMap<Score, ScoreResponseDTO>();

        }
    }

}
