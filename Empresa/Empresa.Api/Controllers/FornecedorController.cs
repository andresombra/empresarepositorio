using Empresa.Application.DTOs;
using Empresa.Application.Interfaces;
using Empresa.Application.Validators;
using Empresa.Domain.Response;
using GerEmpresa.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Empresa.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FornecedorController : ControllerBase
    {
        private readonly IFornecedorService _fornecedorService;
        private readonly ILogger<FornecedorController> _logger;

        public FornecedorController(IFornecedorService fornecedorService, ILogger<FornecedorController> logger)
        {
            _fornecedorService = fornecedorService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ResponseEndpoint<string>> Create([FromBody] FornecedorDto dto)
        {
            ResponseEndpoint<string> response = new();
            var validator = new FornecedorDtoValidator().Validate(dto);
            if (!validator.IsValid)
            {
                response.Mensagem.ResultCode = (int)Empresa.Domain.Enums.ValidationMessageType.Warning;
                foreach (var error in validator.Errors)
                    response.Mensagem.Validacoes.Add(error.ErrorMessage);

                response.StatusCode = HttpStatusCode.BadRequest;
                response.Data = string.Empty;
                return response;
            }

            await _fornecedorService.CriarAsync(dto);
            response.StatusCode = HttpStatusCode.Created;
            response.Mensagem.ResultCode = (int)Empresa.Domain.Enums.ValidationMessageType.Success;
            response.Data = "Fornecedor criado com sucesso.";
            return response;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<FornecedorDto>> Get(int id)
        {
            var dto = await _fornecedorService.ObterPorIdAsync(id);
            if (dto == null) return NotFound();
            return Ok(dto);
        }

        [HttpGet]
        public async Task<ActionResult<IList<FornecedorDto>>> List()
        {
            var lista = await _fornecedorService.ListarAsync();
            return Ok(lista);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] FornecedorDto dto)
        {
            var validator = new FornecedorDtoValidator().Validate(dto);
            if (!validator.IsValid) return BadRequest(validator.Errors.Select(e => e.ErrorMessage));

            try
            {
                await _fornecedorService.AtualizarAsync(id, dto);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _fornecedorService.DeletarAsync(id);
            return NoContent();
        }
    }
}
