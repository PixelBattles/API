using Microsoft.Extensions.DependencyInjection;
using PixelBattles.Server.DataStorage.Models;

namespace PixelBattles.Server.DataStorage.Stores
{
    [Register(typeof(IGameStore))]
    public class GameStore : BaseStore<GameEntity>, IGameStore
    {
        public GameStore(
            PixelBattlesDbContext context) : base(
                context: context)
        {

        }
    }
}
