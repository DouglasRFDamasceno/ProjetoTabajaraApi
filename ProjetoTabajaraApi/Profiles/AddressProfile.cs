using AutoMapper;
using ProjetoTabajaraApi.Data.Dtos.Address;
using ProjetoTabajaraApi.Models;

namespace ProjetoTabajaraApi.Profiles;

public class AddressProfile : Profile
{
    public AddressProfile()
    {
        CreateMap<CreateAddressDto, Address>();
        CreateMap<Address, ReadAddressDto>();
        CreateMap<ReadAddressDto, Address>();
        CreateMap<Address, UpdateAddressDto>();
        CreateMap<UpdateAddressDto, Address>();
    }
}
