using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Empresa.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TempController : Controller
    {
        [HttpGet("public")]
        [AllowAnonymous] // Permite acesso público a este endpoint específico
        public IActionResult Teste()
        {
            return Ok("This is a public endpoint.");
        }
    }
}
