using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using BookMatch.Data;
using BookMatch.Repositories.Interfaces;

namespace BookMatch.Repositories.Implementations
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly BookMatchContext _context;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(BookMatchContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<T?> GetByIdAsync(int id)
            => await _dbSet.FindAsync(id);

        public async Task<IEnumerable<T>> GetAllAsync()
            => await _dbSet.ToListAsync();

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
            => await _dbSet.Where(predicate).ToListAsync();

        public async Task AddAsync(T entity)
            => await _dbSet.AddAsync(entity);

        public void Update(T entity)
            => _dbSet.Update(entity);

        public void Delete(T entity)
            => _dbSet.Remove(entity);

        public async Task<int> SaveChangesAsync()
            => await _context.SaveChangesAsync();
    }
}
