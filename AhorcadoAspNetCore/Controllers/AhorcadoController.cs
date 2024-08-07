using Microsoft.AspNetCore.Mvc;

namespace AhorcadoAspNetCore.Controllers
{
    public class AhorcadoController : Controller
    {
        public IActionResult Ahorcado()
        {
            return View();
        }
    }
}
