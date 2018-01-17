using Microsoft.Extensions.DependencyInjection;
using PixelBattles.Server.DataStorage.Models;

namespace PixelBattles.Server.DataStorage.Stores
{
    [Register(typeof(IUserBattleStore))]
    public class UserBattleStore : BaseStore<UserBattleEntity>, IUserBattleStore
    {
        public UserBattleStore(
            PixelBattlesDbContext context) : base(
                context: context)
        {

        }
    }
}
