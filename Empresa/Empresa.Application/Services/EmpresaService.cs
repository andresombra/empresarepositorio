using Empresa.Application.DTOs;
using Empresa.Application.DTOs.Response;
using Empresa.Domain.Interfaces.Repositories;
using GerEmpresa.Domain.Entities;
using MapsterMapper;

namespace Empresa.Application.Services;

public class EmpresaService : IEmpresaService
{
    private readonly IEmpresaRepository _repo;
    private readonly IMapper _mapper;

    public EmpresaService(IEmpresaRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<IEnumerable<EmpresaResponseDto>> GetAllAsync()
    {
        var listaEmpresa = await _repo.GetAllAsync();
        return _mapper.Map<IEnumerable<EmpresaResponseDto>>(listaEmpresa);
    }

    public async Task<EmpresaResponseDto?> GetByIdAsync(int id)
    {
        var e = await _repo.GetByIdAsync(id);
        if (e == null) return null;
        return new EmpresaResponseDto
        {
            EmpresaId = e.Id,
            Nome = e.Nome!,
            Email = e.Email!,
            DataCadastro = e.DataCadastro,
            Contato = e.Contato!,
            Endereco = e.Endereco!,
            Usuarios = e.Usuarios?
                       .Select(u => new UsuarioResponseDto
                       {
                           Id = u.Id,
                           EmpresaId = u.EmpresaId,
                           Email = u.Email ?? string.Empty,
                           Data = u.Data ?? string.Empty,
                           Situacao = u.Situacao ?? string.Empty,
                           Plano = u.Plano,
                           Adm = u.Adm
                       })
                       .ToList() ?? new List<UsuarioResponseDto>()
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
