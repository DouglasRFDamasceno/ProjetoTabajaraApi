using AutoMapper;
using Castle.Core.Resource;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ProjetoTabajaraApi.Data.Dtos.User;
using ProjetoTabajaraApi.Services;
using System.Net.Http.Headers;
using System.Text;

namespace ProjetoTabajaraApi.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class UserController : ControllerBase
    {
        public UserService _userService;
        private IConfiguration _configuration;

        public UserController(
            UserService userService,
            IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        [HttpPost("create"), Authorize]
        public async Task<IActionResult> CreateUser(CreateUserDto userDto)
        {
            await _userService.CreateUser(userDto);

            return Ok("Usuário cadastrado com sucesso!");
        }

        [HttpGet(""), Authorize]
        public IActionResult GetUsers(int skip = 0, int take = 50)
        {
            var usersDto = _userService.GetUsers(skip, take);

            return Ok(usersDto);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchUser(string id, [FromBody] JsonPatchDocument<UpdateUserDto> patch)
        {
            await Console.Out.WriteLineAsync(id);

            return Ok();
            //return await _userService.PatchUser(patch);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                // Return unauthorized status if the Authorization header is missing
                return Unauthorized();
            }

            string authHeader = Request.Headers["Authorization"];

            if (!AuthenticationHeaderValue.TryParse(authHeader, out var headerValue) || headerValue.Scheme != "Basic")
            {
                // Return unauthorized status if the Authorization header is not using Basic authentication
                return Unauthorized();
            }

            string credentials = Encoding.UTF8.GetString(Convert.FromBase64String(headerValue.Parameter ?? ""));
            string[] parts = credentials.Split(':', 2);

            if (parts.Length != 2)
            {
                // Return unauthorized status if the credentials are invalid
                return Unauthorized();
            }

            string userName = parts[0];
            string password = parts[1];

            LoginUserDto loginDto = new LoginUserDto
            {
                UserName = userName,
                Password = password
            };

            var token = await _userService.Login(loginDto);

            return Ok(new
            {
                jwt = token,
                userName,
                expirationInMinutes = int.Parse(_configuration["tokenExpirationInMinutes"])
            }) ; // Return the token as response
        }
    }
}
