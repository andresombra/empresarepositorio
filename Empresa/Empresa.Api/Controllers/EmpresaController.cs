using Empresa.Application.DTOs;
using Empresa.Application.DTOs.Response;
using Empresa.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class EmpresaController : ControllerBase
{
    private readonly IEmpresaService _service;

    public EmpresaController(IEmpresaService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<EmpresaResponseDto>>> Get()
        => Ok(await _service.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<ActionResult<EmpresaResponseDto>> Get(int id)
    {
        var empresa = await _service.GetByIdAsync(id);
        if (empresa == null) return NotFound();
        return Ok(empresa);
    }

    [HttpPost]
    public async Task<ActionResult<EmpresaResponseDto>> Post([FromBody] EmpresaDto dto)
    {
        var empresa = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(Get), new { id = empresa.EmpresaId }, empresa);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] EmpresaDto dto)
    {
        var updated = await _service.UpdateAsync(id, dto);
        if (!updated) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _service.DeleteAsync(id);
        if (!deleted) return NotFound();
        return NoContent();
    }
}
