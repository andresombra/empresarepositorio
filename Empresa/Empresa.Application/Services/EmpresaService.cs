using Empresa.Application.DTOs;
using Empresa.Application.DTOs.Response;
using Empresa.Domain.Entities;
using Empresa.Domain.Interfaces.Repositories;

public class EmpresaService : IEmpresaService
{
    private readonly IEmpresaRepository _repo;

    public EmpresaService(IEmpresaRepository repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<EmpresaResponseDto>> GetAllAsync()
    {
        var empresas = await _repo.GetAllAsync();
        return empresas.Select(e => new EmpresaResponseDto
        {
            EmpresaId = e.Id,
            Nome = e.Nome,
            Email = e.Email,
            DataCadastro = e.DataCadastro,
            Contato = e.Contato,
            Endereco = e.Endereco
        });
    }

    public async Task<EmpresaResponseDto?> GetByIdAsync(int id)
    {
        var e = await _repo.GetByIdAsync(id);
        if (e == null) return null;
        return new EmpresaResponseDto
        {
            EmpresaId = e.Id,
            Nome = e.Nome,
            Email = e.Email,
            DataCadastro = e.DataCadastro,
            Contato = e.Contato,
            Endereco = e.Endereco
        };
    }

    public async Task<EmpresaResponseDto> CreateAsync(EmpresaDto dto)
    {
        var empresa = new GerEmpresa.Domain.Entities.Empresa
        {
            Nome = dto.Nome,
            Email = dto.Email,
            DataCadastro = dto.DataCadastro,
            Contato = dto.Contato,
            Endereco = dto.Endereco
        };
        await _repo.AddAsync(empresa);
        return new EmpresaResponseDto
        {
            EmpresaId = empresa.Id,
            Nome = empresa.Nome,
            Email = empresa.Email,
            DataCadastro = empresa.DataCadastro,
            Contato = empresa.Contato,
            Endereco = empresa.Endereco
        };
    }

    public async Task<bool> UpdateAsync(int id, EmpresaDto dto)
    {
        var empresa = await _repo.GetByIdAsync(id);
        if (empresa == null) return false;
        empresa.Nome = dto.Nome;
        empresa.Email = dto.Email;
        empresa.DataCadastro = dto.DataCadastro;
        empresa.Contato = dto.Contato;
        empresa.Endereco = dto.Endereco;
        await _repo.UpdateAsync(empresa);
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var empresa = await _repo.GetByIdAsync(id);
        if (empresa == null) return false;
        await _repo.DeleteAsync(empresa);
        return true;
    }
}
