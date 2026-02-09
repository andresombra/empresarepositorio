using Empresa.Domain.Interfaces.Repositories;
using GerEmpresa.Domain.Entities;
using Empresa.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Empresa.Infrastructure.Repositories
{
    public class FornecedorRepository : IFornecedorRepository
    {
        private readonly EmpresaDbContext _context;

        public FornecedorRepository(EmpresaDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Fornecedor fornecedor)
        {
            await _context.Set<Fornecedor>().AddAsync(fornecedor);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Fornecedor fornecedor)
        {
            _context.Set<Fornecedor>().Update(fornecedor);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity == null) return;
            _context.Set<Fornecedor>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<Fornecedor?> GetByIdAsync(int id)
        {
            return await _context.Set<Fornecedor>().FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<IList<Fornecedor>> ListAsync()
        {
            return await _context.Set<Fornecedor>().ToListAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Set<Fornecedor>().AnyAsync(f => f.Id == id);
        }
    }
}
