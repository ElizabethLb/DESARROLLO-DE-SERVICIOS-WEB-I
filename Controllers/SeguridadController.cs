using Microsoft.AspNetCore.Mvc;

namespace BookMatch.Controllers
{
    public class SeguridadController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Title"] = "Seguridad";
            return View();
        }
    }
}
