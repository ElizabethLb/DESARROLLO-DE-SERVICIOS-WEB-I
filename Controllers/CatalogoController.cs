using Microsoft.AspNetCore.Mvc;

namespace BookMatch.Controllers
{
    public class CatalogoController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Title"] = "Catálogo de Libros";
            return View();
        }
    }
}
