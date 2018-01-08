using Microsoft.AspNetCore.Mvc;

namespace PixelBattles.Server.Hub.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
