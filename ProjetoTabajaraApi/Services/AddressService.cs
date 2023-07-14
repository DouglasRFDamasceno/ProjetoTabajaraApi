using AutoMapper;
using ProjetoTabajaraApi.Data;
using ProjetoTabajaraApi.Data.Dtos.Address;
using ProjetoTabajaraApi.Models;

namespace ProjetoTabajaraApi.Services;

public class AddressService
{
    private readonly IMapper _mapper;
    private readonly appDbContext _context;

    public AddressService(IMapper mapper, appDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public string CreateAddress(CreateAddressDto addressDto)
    {
        Address address = _mapper!.Map<Address>(addressDto);

        _context.Adresses.Add(address);
        _context.SaveChanges();

        if (address == null)
        {
            throw new ApplicationException("Falha ao cadastrar o estudante");
        }

        return "Endereço cadastrado com sucesso.";
    }

    public ReadAddressDto? GetAddress(int id)
    {
        Address? address = _context!.Adresses?.FirstOrDefault(address => address.Id == id);

        if (address == null)
        {
            return null;
        }

        ReadAddressDto addressDto = _mapper.Map<ReadAddressDto>(address);
        return addressDto;
    }

    public UpdateAddressDto? UpdateAddress(int id, UpdateAddressDto addressDto)
    {
        var address = _context!.Adresses?.FirstOrDefault(address => address.Id == id);

        if (address == null) return null;

        _mapper.Map(addressDto, address);
        _context.SaveChanges();

        return addressDto;
    }
}
