using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetoTabajaraApi.Data.Dtos.User;
using ProjetoTabajaraApi.Services;
using System.Net.Http.Headers;
using System.Runtime.Intrinsics.X86;
using System.Text;
using static System.Net.WebRequestMethods;

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
        [Authorize]
        public async Task<IActionResult> CreateUser(CreateUserDto userDto)
        {
            await _userService.CreateUser(userDto);

            return Ok("Usuário cadastrado com sucesso!");
        }

        //[HttpPost("login")]
        //public async Task<string> Login([FromHeader] string UserName, [FromHeader] string Password)
        //{
        //    LoginUserDto loginDto = new LoginUserDto
        //    {
        //        UserName = UserName,
        //        Password = Password
        //    };

        //    Console.Write($"{UserName} e ${Password}");
        //    var token = await _userService.Login(loginDto);
        //    return token;
        //}

        [HttpPost("login")]
        public async Task<IActionResult> Login()
        {
            await Console.Out.WriteLineAsync(Request.Headers["Authorization"]);
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
            }); // Return the token as response
        }
    }
}
