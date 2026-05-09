using Empresa.Application.DTOs;
using Empresa.Application.Interfaces;
using Empresa.Domain.Interfaces.Repositories;
using GerEmpresa.Domain.Entities;
using Mapster;

namespace Empresa.Application.Services
{
    public class FornecedorService : IFornecedorService
    {
        private readonly IFornecedorRepository _fornecedorRepository;

        public FornecedorService(IFornecedorRepository fornecedorRepository)
        {
            _fornecedorRepository = fornecedorRepository;
        }

        public async Task CriarAsync(FornecedorDto dto)
        {
            var entity = dto.Adapt<Fornecedor>();
            entity.DataCadastro = DateTime.UtcNow;
            await _fornecedorRepository.AddAsync(entity);
        }

        public async Task AtualizarAsync(int id, FornecedorDto dto)
        {
            var existing = await _fornecedorRepository.GetByIdAsync(id);
            if (existing == null) throw new KeyNotFoundException("Fornecedor não encontrado");

            existing.Nome = dto.Nome;
            existing.Email = dto.Email ?? string.Empty;
            existing.Cnpj = dto.Cnpj ?? string.Empty;
            existing.Contato = dto.Contato ?? string.Empty;
            existing.Endereco = dto.Endereco ?? string.Empty;

            await _fornecedorRepository.UpdateAsync(existing);
        }

        public async Task DeletarAsync(int id)
        {
            await _fornecedorRepository.DeleteAsync(id);
        }

        public async Task<FornecedorDto?> ObterPorIdAsync(int id)
        {
            var entity = await _fornecedorRepository.GetByIdAsync(id);
            if (entity == null) return null;
            return entity.Adapt<FornecedorDto>();
        }

        public async Task<IList<FornecedorDto>> ListarAsync()
        {
            var lista = await _fornecedorRepository.ListAsync();
            return lista.Adapt<IList<FornecedorDto>>();
        }
    }
}
