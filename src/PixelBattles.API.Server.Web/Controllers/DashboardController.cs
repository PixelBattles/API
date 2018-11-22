using Microsoft.AspNetCore.Mvc;
using PixelBattles.API.Server.BusinessLogic.Managers;
using PixelBattles.API.Server.BusinessLogic.Models;
using System;
using System.Threading.Tasks;

namespace PixelBattles.API.Server.Web.Controllers
{
    public class DashboardController : Controller
    {
        private IBattleManager battleManager;

        public DashboardController(IBattleManager battleManager)
        {
            this.battleManager = battleManager ?? throw new ArgumentNullException(nameof(battleManager));
        }

        public async Task<IActionResult> Battles()
        {
            var battles = await battleManager.GetBattlesAsync(new BattleFilter());
            return View(battles);
        }
        
        public IActionResult Battle(Guid id)
        {
            return View(id);
        }
    }
}
