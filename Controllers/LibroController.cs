using BookMatch.Data;
using BookMatch.Models;
using BookMatch.Repositories.Interfaces;
using BookMatch.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookMatch.Controllers
{
    public class LibroController : Controller
    {
        private readonly ILibroService _libroService;
        private readonly BookMatchContext _context;

        public LibroController(ILibroService libroService, BookMatchContext context)
        {
            _libroService = libroService;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Catalogo(FiltroCatalogo filtro)
        {
            filtro ??= new FiltroCatalogo();
            if (filtro.Pagina < 1) filtro.Pagina = 1;
            if (filtro.TamanioPagina < 1) filtro.TamanioPagina = 12;

            var (libros, total) = await _libroService.ObtenerCatalogoAsync(filtro);
            var librosList = libros.ToList();

            var categorias = await _context.Categorias
                .Where(c => c.Activo == 1)
                .OrderBy(c => c.Nombre)
                .ToListAsync();

            var idiomas = await _context.Idiomas
                .OrderBy(i => i.Nombre)
                .ToListAsync();

            var libroIds = librosList.Select(l => l.LibroID).ToList();
            var autorIds = librosList.Select(l => l.AutorID).Distinct().ToList();

            var autores = await _context.Usuarios
                .Where(u => autorIds.Contains(u.UsuarioID))
                .ToDictionaryAsync(u => u.UsuarioID, u => $"{u.Nombre} {u.Apellido}");

            var valoraciones = await _context.Valoraciones
                .Where(v => libroIds.Contains(v.LibroID))
                .GroupBy(v => v.LibroID)
                .Select(g => new
                {
                    LibroID = g.Key,
                    Promedio = g.Average(v => v.Puntuacion),
                    Total = g.Count()
                })
                .ToDictionaryAsync(x => x.LibroID);

            var categoriaNombres = categorias.ToDictionary(c => c.CategoriaID, c => c.Nombre);
            var idiomaNombres = idiomas.ToDictionary(i => i.IdiomaID, i => i.Nombre);

            var vm = new CatalogoViewModel
            {
                Filtro = filtro,
                Total = total,
                Categorias = categorias,
                Idiomas = idiomas,
                Libros = librosList.Select(l => new LibroCatalogoItemVM
                {
                    LibroID = l.LibroID,
                    Titulo = l.Titulo,
                    Portada = l.Portada,
                    Precio = l.Precio,
                    EsGratuito = l.EsGratuito == 1,
                    Paginas = l.Paginas,
                    CategoriaNombre = categoriaNombres.TryGetValue(l.CategoriaID, out var cn) ? cn : "Sin categoría",
                    IdiomaNombre = idiomaNombres.TryGetValue(l.IdiomaID, out var idn) ? idn : "—",
                    AutorNombre = autores.TryGetValue(l.AutorID, out var an) ? an : "Autor desconocido",
                    ValoracionPromedio = valoraciones.TryGetValue(l.LibroID, out var val) ? Math.Round(val.Promedio, 1) : 0,
                    TotalValoraciones = valoraciones.TryGetValue(l.LibroID, out var val2) ? val2.Total : 0
                }).ToList()
            };

            return View(vm);
        }
    }
}
