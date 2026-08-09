using Microsoft.AspNetCore.Mvc;

namespace BookMatch.Controllers
{
    public class ConsultasController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Title"] = "Consultas";
            return View();
        }
    }
}
