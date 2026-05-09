using System.Linq.Expressions;

namespace Empresa.Domain.Interfaces.Repositories
{
    public interface IRepository<T> : IDisposable where T : class
    {
        // Nome usado em serviços/tests
        Task AddAsync(T item);

        // Mantém compatibilidade com possíveis usos antigos
        Task<T> InsertAsync(T item);
        Task<T> UpdateAsync(T item);
        Task<ICollection<T>> UpdateRangeAsync(ICollection<T> items);
        Task DeleteAsync(T item);
        // Remover por id
        Task DeleteAsync(int id);
        Task DeleteRangeAsync(IEnumerable<T> itens);
        Task<T?> GetAsync(Expression<Func<T, bool>> expression);

        // Busca por id primário (int)
        Task<T?> GetByIdAsync(int id);
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression);
        Task<List<TResult>> GetAllAsync<TResult>(Expression<Func<T, bool>> expSearch, Expression<Func<T, TResult>> expSelect);
        Task BeginTransactionAsync();
        Task RollbackTransactionAsync();
        Task<bool> CommitTransactionAsync();

        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);

        Task<TResult> FirstAsync<TResult>(Expression<Func<T, bool>> expSearch, Expression<Func<T, TResult>> expSelect);

        Task InsertRangeAsync(IList<T> itens);

        // Lista todos os registros
        Task<IList<T>> ListAsync();

        #region UnitOfWork
        void Update(T item);
        void UpdateRange(ICollection<T> itens);
        #endregion

    }

}
