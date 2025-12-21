using AutoMapper;
using StudentMN.DTOs.Request;
using StudentMN.DTOs.Response;
using StudentMN.Models.Entities.Account;
using StudentMN.Models.Entities.Class;
using StudentMN.Models.Entities.PermissionModels;
using StudentMN.Models.Entities.ScoreStudent;

namespace StudentMN.Mapping
{
    public class AutoMapperConfigurationProfile : Profile
    {
        public AutoMapperConfigurationProfile()
        {
            CreateMap<Role, RoleResponseDTO>();
            CreateMap<RoleRequestDTO, Role>();
            CreateMap<User, UserResponseDTO>();
            CreateMap<UserRequestDTO, User>();
            CreateMap<Student, StudentResponseDTO>();
            CreateMap<StudentRequestDTO, Student>();
            CreateMap<Teacher, TeacherResponseDTO>();
            CreateMap<TeacherRequestDTO, Teacher>();
            CreateMap<MajorRequestDTO, Major>();
            CreateMap<Major, MajorResponseDTO>();
            CreateMap<ClassesRequestDTO, Classes>();
            CreateMap<Classes, ClassesResponseDTO>();
            CreateMap<SubjectRequestDTO, Subject>();
            CreateMap<Subject, SubjectResponseDTO>();
            CreateMap<ScoreRequestDTO, Score>();
            CreateMap<Score, ScoreResponseDTO>();
            CreateMap<PermissionRequestDTO, Permission>();
            CreateMap<Permission,PermissionDTO>();
            CreateMap<RolePermissionRequestDTO, RolePermission>();
            CreateMap<RolePermission, RolePermissionDTO>();
            CreateMap<CourseSectionRequestDTO, CourseSection>();
            CreateMap<CourseSection, CourseSectionResponseDTO>();
            CreateMap<EnrollmentCourseSection, EnrollmentResponseDTO>();
            CreateMap<EnrollmentRequestDTO, EnrollmentCourseSection>();

        }
    }

}
