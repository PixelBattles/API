using Microsoft.AspNetCore.Mvc;

namespace PixelBattles.API.Server.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
