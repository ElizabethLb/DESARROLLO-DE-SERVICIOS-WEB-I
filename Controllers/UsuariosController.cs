using Microsoft.AspNetCore.Mvc;

namespace BookMatch.Controllers
{
    public class UsuariosController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Title"] = "Gestión de Usuarios";
            return View();
        }
    }
}
