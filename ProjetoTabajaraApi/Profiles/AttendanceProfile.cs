using AutoMapper;
using ProjetoTabajaraApi.Data.Dtos.Attendance;
using ProjetoTabajaraApi.Models;

namespace ProjetoTabajaraApi.Profiles
{
    public class AttendanceProfile : Profile
    {
        public AttendanceProfile()
        {
            CreateMap<CreateAttendanceDto, Attendance>();
            CreateMap<Attendance, ReadAttendanceDto>();
        }

    }
}
