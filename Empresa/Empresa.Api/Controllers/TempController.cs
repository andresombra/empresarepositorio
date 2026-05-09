using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Empresa.Api.Controllers
{
    /// <summary>
    /// Controlador para gerenciar operações relacionadas a empresas.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class TempController : Controller
    {
        /// <summary>
        /// Get public.
        /// </summary>
        [HttpGet("public")]
        [AllowAnonymous] // Permite acesso público a este endpoint específico
        public IActionResult Teste()
        {
            return Ok("This is a public endpoint.");
        }
    }
}
