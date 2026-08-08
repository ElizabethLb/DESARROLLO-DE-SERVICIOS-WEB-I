using BookMatch.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BookMatch.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // TODO: aquí va tu lógica real de validación
            // (buscar el Usuario en la base de datos, verificar contraseña, etc.)
            if (model.Email == "admin@bookmatch.com" && model.Password == "password123")
            {
                // login exitoso -> redirige al dashboard
                return RedirectToAction("Dashboard");
            }

            ModelState.AddModelError("", "Correo o contraseña incorrectos.");
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}