using System.Linq.Expressions;

namespace BookMatch.Repositories.Interfaces
{
    /// <summary>
    /// Contrato base de CRUD que reutilizan todos los repositorios específicos.
    /// </summary>
    public interface IGenericRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<int> SaveChangesAsync();
    }
}
