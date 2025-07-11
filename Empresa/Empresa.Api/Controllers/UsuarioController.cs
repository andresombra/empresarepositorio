using Empresa.Application.DTOs;
using Empresa.Application.Interfaces;
using Empresa.Application.Validators;
using Empresa.Domain.Enums;
using Empresa.Domain.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;

namespace Empresa.Api.Controllers
{
    /// <summary>
    /// Controlador para gerenciar operações relacionadas a empresas.
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioService _usuarioService;
        private readonly ILogger<UsuarioController> _logger;

        /// <summary>
        /// Controlador Usuario.
        /// </summary>
        public UsuarioController(IUsuarioService usuarioService, ILogger<UsuarioController> logger)
        {
            _usuarioService = usuarioService;
            _logger = logger;
        }

        /// <summary>
        /// Post.
        /// </summary>
        [HttpPost]
        public async Task<ResponseEndpoint<string>> Post([FromBody] UsuarioDto dto)
        {
            ResponseEndpoint<string> response = new();
            var dadosUsuario = new UsuarioDto();

            // Exemplo de validação (substitua por sua lógica de validação)
            var resultValidator = new UsuarioDtoValidator().Validate(dto);

            if (!resultValidator.IsValid)
            {
                response.Mensagem.ResultCode = (int)ValidationMessageType.Warning;

                foreach (var item in resultValidator.Errors)
                {
                    response.Mensagem.Validacoes.Add(item.ErrorMessage);
                }

                response.Data = JsonConvert.SerializeObject(dadosUsuario);
                response.StatusCode = HttpStatusCode.BadRequest;

                return response;
            }

            try
            {
                await _usuarioService.CriarUsuarioAsync(dto);
                _logger.LogInformation("Usuário criado com sucesso.");

                response.Data = "Usuário criado com sucesso.";
                response.StatusCode = HttpStatusCode.Created;
                response.Mensagem.ResultCode = (int)ValidationMessageType.Success;

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar usuário.");

                response.Data = "Erro interno do servidor.";
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Mensagem.ResultCode = (int)ValidationMessageType.Error;

                return response;
            }
        }

        /// <summary>
        /// Login.
        /// </summary>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto dto)
        {
            var result = await _usuarioService.AutenticarAsync(dto);
            if (result == null) return Unauthorized("Login inválido.");
            return Ok(result);
        }

        /// <summary>
        /// Get public.
        /// </summary>
        [HttpGet("public")]
        [AllowAnonymous] // Permite acesso público a este endpoint específico
        public ResponseEndpoint<string> GetPublic()
        {
            ResponseEndpoint<string> response = new();

            try
            {
                _logger.LogInformation("Public GET request received.");

                response.Data = "This is a public endpoint.";
                response.StatusCode = HttpStatusCode.OK;
                response.Mensagem.ResultCode = (int)ValidationMessageType.Success;

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao executar GetPublic.");

                response.Data = "Erro ao executar GetPublic.";
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Mensagem.ResultCode = (int)ValidationMessageType.Error;

                return response;
            }
        }
    }
}
