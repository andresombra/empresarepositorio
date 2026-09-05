using Empresa.Domain.Interfaces.Repositories;
using Empresa.Infrastructure.Data;
using Empresa.Domain.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace Empresa.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly EmpresaDbContext _context;
    private IDbContextTransaction? _transaction;

    public UnitOfWork(EmpresaDbContext context)
    {
        _context = context;
    }

    public async Task BeginTransaction()
    {
        _transaction = await _context.Database.BeginTransactionAsync();
    }

    public async Task Commit()
    {
        await _context.SaveChangesAsync();
        await _transaction!.CommitAsync();
    }

    public async Task Rollback()
    {
        await _transaction!.RollbackAsync();
    }

    public IEmpresaRepository EmpresaRepository => new EmpresaRepository(_context);
    public IFornecedorRepository FornecedorRepository => new FornecedorRepository(_context);
    public IUsuarioRepository UsuarioRepository => new UsuarioRepository(_context);
}
