using Microsoft.AspNetCore.Mvc;
using PixelBattles.API.Server.BusinessLogic.Battles;
using PixelBattles.API.Server.BusinessLogic.Battles.Models;
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
        
        public IActionResult Battle(long id)
        {
            return View(id);
        }
    }
}
