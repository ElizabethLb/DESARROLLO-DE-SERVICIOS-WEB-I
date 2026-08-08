using BookMatch.Models;

namespace BookMatch.Repositories.Interfaces
{
    public class FiltroCatalogo
    {
        public string? Busqueda { get; set; }
        public int? CategoriaId { get; set; }
        public int? IdiomaId { get; set; }
        public bool? SoloGratis { get; set; }
        public decimal? ValoracionMinima { get; set; }
        public int Pagina { get; set; } = 1;
        public int TamanioPagina { get; set; } = 12;
    }

    public interface ILibroRepository : IGenericRepository<Libro>
    {
        
        Task<(IEnumerable<Libro> Libros, int Total)> ObtenerCatalogoAsync(FiltroCatalogo filtro);
        Task<Libro?> ObtenerDetalleAsync(int libroId);
        Task<int> PublicarLibroAsync(Libro libro);
        Task<IEnumerable<Libro>> GetByAutorAsync(int usuarioId);
        Task<IEnumerable<Libro>> GetByCategoriaAsync(int categoriaId);
    }
}
