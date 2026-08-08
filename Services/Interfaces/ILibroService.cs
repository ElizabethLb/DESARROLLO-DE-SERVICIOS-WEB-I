using BookMatch.Models;
using BookMatch.Repositories.Interfaces;

namespace BookMatch.Services.Interfaces
{
    public interface ILibroService
    {
        Task<(IEnumerable<Libro> Libros, int Total)> ObtenerCatalogoAsync(FiltroCatalogo filtro);
        Task<Libro?> ObtenerDetalleAsync(int libroId);
        Task<int> PublicarLibroAsync(Libro libro);
        Task<IEnumerable<Libro>> GetByAutorAsync(int usuarioId);
        Task<IEnumerable<Libro>> GetByCategoriaAsync(int categoriaId);
    }
}
