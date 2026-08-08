using BookMatch.Models;
using BookMatch.Repositories.Interfaces;
using BookMatch.Services.Interfaces;

namespace BookMatch.Services.Implementations
{
    public class LibroService : ILibroService
    {
        private readonly ILibroRepository _libroRepository;

        public LibroService(ILibroRepository libroRepository)
        {
            _libroRepository = libroRepository;
        }

        public Task<(IEnumerable<Libro> Libros, int Total)> ObtenerCatalogoAsync(FiltroCatalogo filtro)
            => _libroRepository.ObtenerCatalogoAsync(filtro);

        public Task<Libro?> ObtenerDetalleAsync(int libroId)
            => _libroRepository.ObtenerDetalleAsync(libroId);

        public Task<int> PublicarLibroAsync(Libro libro)
            => _libroRepository.PublicarLibroAsync(libro);

        public Task<IEnumerable<Libro>> GetByAutorAsync(int usuarioId)
            => _libroRepository.GetByAutorAsync(usuarioId);

        public Task<IEnumerable<Libro>> GetByCategoriaAsync(int categoriaId)
            => _libroRepository.GetByCategoriaAsync(categoriaId);
    }
}
