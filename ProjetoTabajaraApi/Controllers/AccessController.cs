using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ProjetoTabajaraApi.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class AccessController : ControllerBase
    {
        [HttpGet]
        [Authorize]
<<<<<<< HEAD
        public IActionResult Get() {
            return Ok("Acesso permitido!");
=======
        public bool Get() {
            return true;
>>>>>>> develop
        }
    }
}
