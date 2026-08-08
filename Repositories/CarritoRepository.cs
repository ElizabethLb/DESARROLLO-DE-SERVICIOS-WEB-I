using Microsoft.EntityFrameworkCore;
using BookMatch.Data;
using BookMatch.Models;
using BookMatch.Repositories.Interfaces;

namespace BookMatch.Repositories.Implementations
{
    public class CarritoRepository : GenericRepository<Carrito>, ICarritoRepository
    {
        public CarritoRepository(BookMatchContext context) : base(context) { }

        public async Task<IEnumerable<Carrito>> GetByUsuarioAsync(int usuarioId)
            => await _dbSet
                .Where(c => c.UsuarioId == usuarioId)
                .Include(c => c.Libro)
                .AsNoTracking()
                .ToListAsync();

        public async Task<bool> AgregarAsync(int usuarioId, int libroId)
        {
            // La tabla Carrito tiene constraint de unicidad usuario/libro (definida en el script SQL):
            // si ya existe, no se duplica.
            var existe = await _dbSet.AnyAsync(c => c.UsuarioId == usuarioId && c.LibroId == libroId);
            if (existe) return false;

            await AddAsync(new Carrito { UsuarioId = usuarioId, LibroId = libroId, FechaAgregado = DateTime.Now });
            return await SaveChangesAsync() > 0;
        }

        public async Task<bool> QuitarAsync(int usuarioId, int libroId)
        {
            var item = await _dbSet.FirstOrDefaultAsync(c => c.UsuarioId == usuarioId && c.LibroId == libroId);
            if (item is null) return false;

            Delete(item);
            return await SaveChangesAsync() > 0;
        }

        public async Task<bool> VaciarCarritoAsync(int usuarioId)
        {
            var items = await _dbSet.Where(c => c.UsuarioId == usuarioId).ToListAsync();
            if (!items.Any()) return true;

            _dbSet.RemoveRange(items);
            return await SaveChangesAsync() > 0;
        }
    }
}
