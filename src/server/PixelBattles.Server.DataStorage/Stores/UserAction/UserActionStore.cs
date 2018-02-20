using Microsoft.Extensions.DependencyInjection;
using PixelBattles.Server.DataStorage.Models;

namespace PixelBattles.Server.DataStorage.Stores
{
    [Register(typeof(IUserActionStore))]
    public class UserActionStore : BaseStore<UserActionEntity>, IUserActionStore
    {
        public UserActionStore(
            PixelBattlesDbContext context) : base(
                context: context)
        {

        }
    }
}
