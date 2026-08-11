using Microsoft.AspNetCore.Mvc;

namespace BookMatch.Controllers
{
    public class ReportesController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Title"] = "Reportes";
            return View();
        }
    }
}
