using Empresa.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public class EmpresaRepository : IEmpresaRepository
{
    private readonly EmpresaDbContext _context;

    public EmpresaRepository(EmpresaDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<GerEmpresa.Domain.Entities.Empresa>> GetAllAsync()
    {
        return await _context.Empresas.ToListAsync();
    }

    public async Task<GerEmpresa.Domain.Entities.Empresa?> GetByIdAsync(int id)
    {
        return await _context.Empresas.FindAsync(id);
    }

    public async Task AddAsync(GerEmpresa.Domain.Entities.Empresa empresa)
    {
        _context.Empresas.Add(empresa);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(GerEmpresa.Domain.Entities.Empresa empresa)
    {
        _context.Empresas.Update(empresa);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(GerEmpresa.Domain.Entities.Empresa empresa)
    {
        _context.Empresas.Remove(empresa);
        await _context.SaveChangesAsync();
    }
}
