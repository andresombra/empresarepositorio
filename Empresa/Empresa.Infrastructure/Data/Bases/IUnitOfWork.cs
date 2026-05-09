using Empresa.Domain.Interfaces.Repositories;

namespace Empresa.Infrastructure.Data.Bases;

public interface IUnitOfWork
{
    Task BeginTransaction();
    Task Commit();
    Task Rollback();

    IEmpresaRepository EmpresaRepository { get; }
    IFornecedorRepository FornecedorRepository { get; }
    IUsuarioRepository UsuarioRepository { get; }
}
