using Empresa.Application.DTOs;
using GerEmpresa.Domain.Entities;

namespace Empresa.Application.Interfaces
{
    public interface IFornecedorService
    {
        Task CriarAsync(FornecedorDto dto);
        Task AtualizarAsync(int id, FornecedorDto dto);
        Task DeletarAsync(int id);
        Task<FornecedorDto?> ObterPorIdAsync(int id);
        Task<IList<FornecedorDto>> ListarAsync();
    }
}
