using Microsoft.AspNetCore.Mvc;

namespace PixelBattles.Server.Web.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
