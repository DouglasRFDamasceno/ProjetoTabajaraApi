using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetoTabajaraApi.Data.Dtos.Address;
using ProjetoTabajaraApi.Services;

namespace ProjetoTabajaraApi.Controllers;

[ApiController]
[Route("[Controller]")]
public class AddressController : ControllerBase
{
    public AddressService _addressService;

    public AddressController(AddressService addressService)
    {
        _addressService = addressService;
    }

    [HttpPost]
    public IActionResult CreateAddress(CreateAddressDto addressDto)
    {
        AccessController access = new ();

        if (!access.Get())
        {
            throw new ApplicationException("Acesso negado!");
        }

        var result = _addressService.CreateAddress(addressDto);

        return Ok(result);
    }

    [HttpPut]
    [Authorize]
    public IActionResult UpdateAddress(int id, [FromBody] UpdateAddressDto addressDto)
    {
        var student = _addressService.UpdateAddress(id, addressDto);

        if (student == null) return NotFound();

        return Ok($"Endereço criado com sucesso.");
    }
}
