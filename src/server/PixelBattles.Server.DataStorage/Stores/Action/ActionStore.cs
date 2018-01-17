using Microsoft.Extensions.DependencyInjection;
using PixelBattles.Server.DataStorage.Models;

namespace PixelBattles.Server.DataStorage.Stores
{
    [Register(typeof(IActionStore))]
    public class ActionStore : BaseStore<ActionEntity>, IActionStore
    {
        public ActionStore(
            PixelBattlesDbContext context) : base(
                context: context)
        {

        }
    }
}
