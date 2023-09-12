using AutoMapper;
using Castle.Core.Resource;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ProjetoTabajaraApi.Data.Dtos.User;
using ProjetoTabajaraApi.Models;
using ProjetoTabajaraApi.Services;
using System.Net.Http.Headers;
using System.Text;

namespace ProjetoTabajaraApi.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    [EnableCors("MyPolicy")]
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

        [HttpPost("create")]
        [Authorize]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto userDto)
        {
            Console.Write("Teste");
            var user = await _userService.CreateUser(userDto);
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult DeleteUser(string id)
        {
            var actionResult = _userService.DeleteUser(id);
            return Ok(actionResult);
        }

        [HttpGet(""), Authorize]
        public IActionResult GetUsers(int skip = 0, int take = 50)
        {
            var usersDto = _userService.GetUsers(skip, take);

            return Ok(usersDto);
        }

        [HttpGet("{id}"), Authorize]
        public IActionResult GetUser(string id)
        {
            ReadUserDto? usersDto = _userService.GetUser(id);

            return Ok(usersDto);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchUser(string id, [FromBody] JsonPatchDocument<UpdateUserDto> patch)
        {
            return _userService.PatchUser(id, patch);
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
