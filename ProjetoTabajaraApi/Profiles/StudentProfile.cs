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
        // Obtém a presença de 30 dias anterios ao dia da busca
        CreateMap<Student, ReadStudentDto>()
            .ForMember(
                studentDto =>
                studentDto.Address,
                opt => opt.MapFrom(student => student.Address)
            ).ForMember(
                studentDto =>
                studentDto.Attendances,
                opt => opt.MapFrom(student =>
                    student.Attendances.Where(attendance => attendance.Date >= DateTime.Now.AddDays(-30))
                )
            );
    }
}
