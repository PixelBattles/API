using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PixelBattles.Server.BusinessLogic.Models;
using PixelBattles.Server.DataStorage.Models;
using PixelBattles.Server.DataStorage.Stores;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PixelBattles.Server.BusinessLogic.Managers
{
    [Register(typeof(IUserBattleManager))]
    public class UserBattleManager : BaseManager, IUserBattleManager
    {
        protected IUserBattleStore UserBattleStore { get; set; }

        public UserBattleManager(
            IUserBattleStore userBattleStore,
            IHttpContextAccessor contextAccessor,
            ErrorDescriber errorDescriber,
            IMapper mapper,
            ILogger<GameManager> logger)
            : base(
                  contextAccessor: contextAccessor,
                  errorDescriber: errorDescriber,
                  mapper: mapper,
                  logger: logger)
        {
            UserBattleStore = userBattleStore ?? throw new ArgumentNullException(nameof(userBattleStore));   
        }
        
        public async Task<UserBattle> GetUserBattleAsync(Guid userId, Guid battleId, CancellationToken cancellationToken = default(CancellationToken))
        {
            ThrowIfDisposed();
            var userBattle = await UserBattleStore.GetUserBattleAsync(userId, battleId, CancellationToken);
            return Mapper.Map<UserBattleEntity, UserBattle>(userBattle);
        }

        public async Task<UserBattle> GetUserBattleFromGameAsync(Guid userId, Guid gameId, CancellationToken cancellationToken = default(CancellationToken))
        {
            ThrowIfDisposed();
            var userBattle = await UserBattleStore.GetUserBattleFromGameAsync(userId, gameId, CancellationToken);
            return Mapper.Map<UserBattleEntity, UserBattle>(userBattle);
        }

        protected override void DisposeStores()
        {
        }
    }
}
