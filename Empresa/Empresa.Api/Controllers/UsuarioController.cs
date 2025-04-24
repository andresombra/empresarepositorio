using Empresa.Application.DTOs;
using Empresa.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Empresa.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioService _usuarioService;
        private readonly ILogger<UsuarioController> _logger;

        public UsuarioController(IUsuarioService usuarioService, ILogger<UsuarioController> logger)
        {
            _usuarioService = usuarioService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UsuarioDto dto)
        {
            await _usuarioService.CriarUsuarioAsync(dto);
            return Ok(new { message = "Usuário criado com sucesso." });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto dto)
        {
            var result = await _usuarioService.AutenticarAsync(dto);
            if (result == null) return Unauthorized("Login inválido.");
            return Ok(result);
        }

        [HttpGet("public")]
        [AllowAnonymous] // Permite acesso público a este endpoint específico
        public IActionResult GetPublic()
        {
            _logger.LogInformation("Public GET request received.");
            return Ok("This is a public endpoint.");
        }
    }
}
