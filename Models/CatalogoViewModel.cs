using BookMatch.Repositories.Interfaces;

namespace BookMatch.Models
{
    public class LibroCatalogoItemVM
    {
        public int LibroID { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string? Portada { get; set; }
        public string Precio { get; set; } = "0.00";
        public bool EsGratuito { get; set; }
        public string CategoriaNombre { get; set; } = string.Empty;
        public string IdiomaNombre { get; set; } = string.Empty;
        public string AutorNombre { get; set; } = string.Empty;
        public int? Paginas { get; set; }
        public double ValoracionPromedio { get; set; }
        public int TotalValoraciones { get; set; }
    }

    public class CatalogoViewModel
    {
        public List<LibroCatalogoItemVM> Libros { get; set; } = new();
        public List<Categoria> Categorias { get; set; } = new();
        public List<Idioma> Idiomas { get; set; } = new();
        public FiltroCatalogo Filtro { get; set; } = new();
        public int Total { get; set; }

        public int TotalPaginas => Filtro.TamanioPagina <= 0
            ? 1
            : (int)Math.Ceiling(Total / (double)Filtro.TamanioPagina);
    }
}
