using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PixelBattles.Server.BusinessLogic.Models;
using PixelBattles.Server.DataStorage.Models;
using PixelBattles.Server.DataStorage.Stores;
using System;
using System.Threading.Tasks;

namespace PixelBattles.Server.BusinessLogic.Managers
{
    [Register(typeof(IBattleManager))]
    public class BattleManager : BaseManager, IBattleManager
    {
        protected IBattleStore BattleStore { get; set; }

        public BattleManager(
            IBattleStore battleStore,
            IHttpContextAccessor contextAccessor,
            ErrorDescriber errorDescriber,
            IMapper mapper,
            ILogger<BattleManager> logger)
            : base(
                  contextAccessor: contextAccessor,
                  errorDescriber: errorDescriber,
                  mapper: mapper,
                  logger: logger)
        {
            BattleStore = battleStore ?? throw new ArgumentNullException(nameof(battleStore));
        }

        public async Task<Battle> GetBattleAsync(Guid battleId)
        {
            ThrowIfDisposed();
            var battle = await BattleStore.GetBattleAsync(battleId, CancellationToken);
            return Mapper.Map<BattleEntity, Battle>(battle);
        }
        
        protected override void DisposeStores()
        {
            BattleStore.Dispose();
        }
    }
}
