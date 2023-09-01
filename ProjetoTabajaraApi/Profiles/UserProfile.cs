using AutoMapper;
using ProjetoTabajaraApi.Data.Dtos.User;
using ProjetoTabajaraApi.Models;

namespace ProjetoTabajaraApi.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<CreateUserDto, User>();
            CreateMap<User, ReadUserDto>();
            CreateMap<ReadUserDto, User>();
            CreateMap<UpdateUserDto, User>();
            CreateMap<User, UpdateUserDto>();
        }
    }
}
