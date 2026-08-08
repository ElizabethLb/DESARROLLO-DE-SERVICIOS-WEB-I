using BookMatch.Models;

namespace BookMatch.Repositories.Interfaces
{
    /// <summary>Filtros del módulo Catálogo (género, precio, idioma, valoración, búsqueda).</summary>
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
        /// <summary>Ejecuta sp_ObtenerCatalogo. Devuelve la página de libros y el total de resultados (para paginación).</summary>
        Task<(IEnumerable<Libro> Libros, int Total)> ObtenerCatalogoAsync(FiltroCatalogo filtro);

        /// <summary>Ejecuta sp_DetalleLibro (incluye valoración promedio, autor, categoría).</summary>
        Task<Libro?> ObtenerDetalleAsync(int libroId);

        /// <summary>Ejecuta sp_PublicarLibro (alta o edición desde Mis Publicaciones).</summary>
        Task<int> PublicarLibroAsync(Libro libro);

        Task<IEnumerable<Libro>> GetByAutorAsync(int usuarioId);
        Task<IEnumerable<Libro>> GetByCategoriaAsync(int categoriaId);
    }
}
