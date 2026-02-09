using Empresa.Domain.Interfaces.Repositories;
using Empresa.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Empresa.Infrastructure.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        protected readonly EmpresaDbContext _context;
        private readonly DbSet<T> _dataset;

        protected BaseRepository(EmpresaDbContext context)
        {
            _context = context;
            _dataset = _context.Set<T>();
        }

        public async Task<T?> GetAsync(Expression<Func<T, bool>> expression)
        {
            return await _dataset.SingleOrDefaultAsync(expression);
        }

        public async Task<TResult> FirstAsync<TResult>(Expression<Func<T, bool>> expSearch, Expression<Func<T, TResult>> expSelect)
        {
            return await _dataset.Where(expSearch).Select(expSelect).FirstAsync();
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression)
        {
            return await _dataset
                .Where(expression)
                .ToListAsync();
        }

        public async Task<List<TResult>> GetAllAsync<TResult>(Expression<Func<T, bool>> expSearch, Expression<Func<T, TResult>> expSelect)
        {
            return await _dataset
                .Where(expSearch)
                .Select(expSelect)
                .ToListAsync();
        }

        public async Task<T> InsertAsync(T item)
        {
            _dataset.Add(item);
            await _context.SaveChangesAsync(CancellationToken.None);
            return item;
        }

        public async Task<ICollection<T>> UpdateRangeAsync(ICollection<T> items)
        {
            _context.UpdateRange(items);
            await _context.SaveChangesAsync(CancellationToken.None);
            return items;
        }

        public async Task<T> UpdateAsync(T item)
        {
            _context.Entry(item).CurrentValues.SetValues(item);
            _dataset.Update(item);
            await _context.SaveChangesAsync(CancellationToken.None);
            return item;
        }

        public async Task DeleteAsync(T item)
        {
            _dataset.Remove(item);
            await _context.SaveChangesAsync(CancellationToken.None);
        }

        public async Task DeleteRangeAsync(IEnumerable<T> itens)
        {
            _dataset.RemoveRange(itens);
            await _context.SaveChangesAsync(CancellationToken.None);
        }

        public async Task BeginTransactionAsync()
        {
            await _context.BeginTransaction();
        }

        public async Task<bool> CommitTransactionAsync()
        {
            return await _context.Commit();
        }

        public async Task RollbackTransactionAsync()
        {
            await _context.Rollback();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            _context?.Dispose();
        }

        public Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
        {
            return _dataset.AnyAsync(expression);
        }

        public async Task InsertRangeAsync(IList<T> itens)
        {
            await _dataset.AddRangeAsync(itens);
            await _context.SaveChangesAsync();
        }

        #region UnitOfWork
        public void Update(T item)
        {
            _dataset.Update(item);
        }
        public void UpdateRange(ICollection<T> itens)
        {
            _dataset.UpdateRange(itens);
        }
        #endregion
    }
}
