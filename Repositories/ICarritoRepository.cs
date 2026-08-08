using BookMatch.Models;

namespace BookMatch.Repositories.Interfaces
{
    public interface ICarritoRepository : IGenericRepository<Carrito>
    {
        Task<IEnumerable<Carrito>> GetByUsuarioAsync(int usuarioId);
        Task<bool> AgregarAsync(int usuarioId, int libroId);
        Task<bool> QuitarAsync(int usuarioId, int libroId);
        Task<bool> VaciarCarritoAsync(int usuarioId);
    }
}
