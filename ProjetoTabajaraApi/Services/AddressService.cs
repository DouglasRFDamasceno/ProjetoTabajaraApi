using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ProjetoTabajaraApi.Data;
using ProjetoTabajaraApi.Data.Dtos.Address;
using ProjetoTabajaraApi.Data.Dtos.Student;
using ProjetoTabajaraApi.Models;

namespace ProjetoTabajaraApi.Services;

public class AddressService : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly appDbContext _context;

    public AddressService(IMapper mapper, appDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public Address CreateAddress(CreateAddressDto addressDto)
    {
        Address address = _mapper!.Map<Address>(addressDto);

        _context.Adresses.Add(address);
        _context.SaveChanges();

        if (address == null)
        {
            throw new ApplicationException("Falha ao cadastrar o estudante");
        }

        return address;
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

    public Address? UpdateAddress(int id, UpdateAddressDto addressDto)
    {
        var address = _context!.Adresses?.FirstOrDefault(address => address.Id == id);

        if (address == null) return null;

        _mapper.Map(addressDto, address);
        _context.SaveChanges();

        return address;
    }

    public bool DeleteAddress(int id)
    {
        var address = _context.Adresses.FirstOrDefault(address => address.Id == id);

        if (address == null) return false;

        _context.Remove(address);
        _context.SaveChanges();

        return true;
    }

    public List<Address>? GetAdresses(int skip, int take)
    {
        List<Address>? adresses = _context!.Adresses?
            .Skip(skip)
            .Take(take)
            .ToList();

        return adresses;
    }

    public IActionResult PatchAddress(string id, JsonPatchDocument<UpdateAddressDto> patch)
    {
        if (string.IsNullOrEmpty(id))
        {
            return BadRequest("O ID do estudante é inválido.");
        }

        Address? address = _context.Adresses.FirstOrDefault(address => address.Id == int.Parse(id));

        if (address == null)
        {
            return NotFound("Estudante não encontrado.");
        }

        UpdateAddressDto addressToUpdate = _mapper.Map<UpdateAddressDto>(address);

        patch.ApplyTo(addressToUpdate, ModelState);

        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        try
        {
            // Mapeamento reverso após a validação do modelo.
            _mapper.Map(addressToUpdate, address);
            _context.SaveChanges();
            return Ok(addressToUpdate); // Retorna um status 204 (No Content) em caso de sucesso.
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Ocorreu um erro ao atualizar o usuário: {ex.Message}");
        }
    }
}
