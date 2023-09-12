using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ProjetoTabajaraApi.Data.Dtos.Address;
using ProjetoTabajaraApi.Models;
using ProjetoTabajaraApi.Services;

namespace ProjetoTabajaraApi.Controllers;

[ApiController]
[Route("api/[Controller]")]
[EnableCors("MyPolicy")]
public class AddressController : ControllerBase
{
    public AddressService _addressService;

    public AddressController(AddressService addressService)
    {
        _addressService = addressService;
    }

    [HttpPost("create")]
    [Authorize]
    public CreatedAtActionResult CreateAddress(CreateAddressDto addressDto)
    {
        var address = _addressService.CreateAddress(addressDto);
        return CreatedAtAction(nameof(GetAddress), new { id = address.Id }, address);
    }

    [HttpGet("{id}")]
    [Authorize]
    public IActionResult GetAddress(int id)
    {
        var address = _addressService.GetAddress(id);

        return Ok(address);
    }

    [HttpGet]
    [Authorize]
    public IActionResult GetAdresses(int skip = 0, int take = 50)
    {
        List<Address>? adresses = _addressService.GetAdresses(skip, take);

        return Ok(adresses);
    }

    [HttpPut("{id}")]
    [Authorize]
    public IActionResult UpdateAddress(int id, [FromBody] UpdateAddressDto addressDto)
    {
        var address = _addressService.UpdateAddress(id, addressDto);

        if (address == null) return NotFound();

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize]
    public IActionResult DeleteAddress(int id)
    {
        var deletedAddress = _addressService.DeleteAddress(id);

        if(!deletedAddress) return NotFound();

        return NoContent();
    }
}
