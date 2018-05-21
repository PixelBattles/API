using Microsoft.AspNetCore.Mvc;
using PixelBattles.Server.BusinessLogic.Managers;
using PixelBattles.Server.BusinessLogic.Models;
using System;
using System.Threading.Tasks;

namespace PixelBattles.Server.Web.Controllers
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
