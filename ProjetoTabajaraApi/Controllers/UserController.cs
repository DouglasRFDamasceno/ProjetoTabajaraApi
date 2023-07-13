using Microsoft.AspNetCore.Mvc;
using ProjetoTabajaraApi.Data.Dtos.User;
using ProjetoTabajaraApi.Services;

namespace ProjetoTabajaraApi.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class UserController : ControllerBase
    {
        public UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateUser(CreateUserDto userDto)
        {
            await _userService.CreateUser(userDto);

            return Ok("Usuário cadastrado com sucesso!");
        }
        
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserDto loginDto)
        {
            var token = await _userService.Login(loginDto);

            return Ok(token);
        }
    }
}
