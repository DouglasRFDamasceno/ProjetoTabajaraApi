using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace ProjetoTabajaraApi.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    [EnableCors("MyPolicy")]
    public class AccessController : ControllerBase
    {
        [HttpGet]
        [Authorize]
        public bool Get() {
            return true;
        }
    }
}
