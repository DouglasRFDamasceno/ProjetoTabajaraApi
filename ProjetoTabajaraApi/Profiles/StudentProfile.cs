using AutoMapper;
using ProjetoTabajaraApi.Data.Dtos.Student;
using ProjetoTabajaraApi.Models;

namespace ProjetoTabajaraApi.Profiles;

public class StudentProfile : Profile
{
    public StudentProfile()
    {
        CreateMap<CreateStudentDto, Student>();
        CreateMap<UpdateStudentDto, Student>();
        CreateMap<Student, UpdateStudentDto>();
        CreateMap<ReadStudentDto, Student>();
        CreateMap<Student, ReadStudentDto>()
            .ForMember(
                studentDto => 
                studentDto.Address,
                opt => opt.MapFrom(student => student.Address)
            );
    }
}
