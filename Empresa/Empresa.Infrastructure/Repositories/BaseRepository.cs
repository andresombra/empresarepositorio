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

        public async Task<T?> GetByIdAsync(int id)
        {
            // Assumindo que as entidades usam propriedade Id do tipo int
            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.PropertyOrField(parameter, "Id");
            var equals = Expression.Equal(property, Expression.Constant(id));
            var lambda = Expression.Lambda<Func<T, bool>>(equals, parameter);
            return await _dataset.FirstOrDefaultAsync(lambda);
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
            await _dataset.AddAsync(item);
            await _context.SaveChangesAsync(CancellationToken.None);
            return item;
        }

        // Compatibilidade com serviços/tests que usam AddAsync
        public async Task AddAsync(T item)
        {
            await InsertAsync(item);
        }

        public async Task<ICollection<T>> UpdateRangeAsync(ICollection<T> items)
        {
            _context.UpdateRange(items);
            await _context.SaveChangesAsync(CancellationToken.None);
            return items;
        }

        public async Task<T> UpdateAsync(T item)
        {
            ArgumentNullException.ThrowIfNull(item);

            _dataset.Update(item);
            await _context.SaveChangesAsync(CancellationToken.None);
            return item;
        }

        public async Task DeleteAsync(T item)
        {
            _dataset.Remove(item);
            await _context.SaveChangesAsync(CancellationToken.None);
        }

        // Remover por id (quando o repositório expõe esse contrato)
        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity == null) return;
            _dataset.Remove(entity);
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

        public async Task<IList<T>> ListAsync()
        {
            return await _dataset.ToListAsync();
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
