using BookMatch.Models;

namespace BookMatch.Services.Interfaces
{
    public interface ICarritoService
    {
        Task<IEnumerable<Carrito>> GetByUsuarioAsync(int usuarioId);
        Task<bool> AgregarAsync(int usuarioId, int libroId);
        Task<bool> QuitarAsync(int usuarioId, int libroId);
        Task<bool> VaciarCarritoAsync(int usuarioId);
    }
}
