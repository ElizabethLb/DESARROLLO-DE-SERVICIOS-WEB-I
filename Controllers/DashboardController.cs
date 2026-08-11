using Microsoft.AspNetCore.Mvc;

namespace BookMatch.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Title"] = "Dashboard";
            return View();
        }
    }
}
