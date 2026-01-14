using Empresa.Application.DTOs;
using Empresa.Application.DTOs.Response;
using Microsoft.AspNetCore.Mvc;

namespace Empresa.Api.Controllers
{
    /// <summary>
    /// Controlador para gerenciar operações relacionadas a empresas.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class EmpresaController : ControllerBase
    {
        private readonly IEmpresaService _service;

        /// <summary>
        /// Controlador Empresas.
        /// </summary>
        public EmpresaController(IEmpresaService service)
        {
            _service = service;
        }

        /// <summary>
        /// Get.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmpresaResponseDto>>> Get()
            => Ok(await _service.GetAllAsync());

        /// <summary>
        /// Get id.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<EmpresaResponseDto>> Get(int id)
        {
            var empresa = await _service.GetByIdAsync(id);
            if (empresa == null) return NotFound();
            return Ok(empresa);
        }

        /// <summary>
        /// Post.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<EmpresaResponseDto>> Post([FromBody] EmpresaDto dto)
        {
            var empresa = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = empresa.EmpresaId }, empresa);
        }

        /// <summary>
        /// Put.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] EmpresaDto dto)
        {
            var updated = await _service.UpdateAsync(id, dto);
            if (!updated) return NotFound();
            return NoContent();
        }

        /// <summary>
        /// Delete.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
