using Empresa.Domain.Interfaces.Repositories;

namespace Empresa.Domain.Interfaces;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task BeginTransaction();
    Task Commit();
    Task Rollback();

    IEmpresaRepository EmpresaRepository { get; }
    IFornecedorRepository FornecedorRepository { get; }
    IUsuarioRepository UsuarioRepository { get; }
}
