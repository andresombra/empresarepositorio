using GerEmpresa.Domain.Entities;

namespace Empresa.Domain.Interfaces.Repositories
{
    public interface IFornecedorRepository
    {
        Task AddAsync(Fornecedor fornecedor);
        Task UpdateAsync(Fornecedor fornecedor);
        Task DeleteAsync(int id);
        Task<Fornecedor?> GetByIdAsync(int id);
        Task<IList<Fornecedor>> ListAsync();
        Task<bool> ExistsAsync(int id);
    }
}
