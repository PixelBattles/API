using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PixelBattles.Server.BusinessLogic.Models;
using PixelBattles.Server.Core;
using PixelBattles.Server.DataStorage.Models;
using PixelBattles.Server.DataStorage.Stores;
using System;
using System.Collections.Generic;
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
        }

        public async Task<CreateBattleResult> CreateBattleAsync(CreateBattleCommand command)
        {
            ThrowIfDisposed();

            if (String.IsNullOrWhiteSpace(command.Name))
            {
                return new CreateBattleResult(new Error("Empty name", "Name can't be empty"));
            }

            //var existingBattle = await BattleStore.GetBattleAsync(command.Name, CancellationToken);
            //if (existingBattle != null)
            //{
            //    return new CreateBattleResult(new Error("Name duplication", "Battle with the same name is already exist"));
            //}

            var battle = new BattleEntity()
            {
                Description = command.Description,
                Name = command.Name
            };

            var result = await BattleStore.CreateBattleAsync(battle, CancellationToken);
            if (result.Succeeded)
            {
                return new CreateBattleResult(battle.BattleId);
            }
            else
            {
                return new CreateBattleResult(result.Errors);
            }
        }

        public async Task<IEnumerable<Battle>> GetBattlesAsync(BattleFilter battleFilter)
        {
            var battleEntityFilter = new BattleEntityFilter
            {
                Name = battleFilter.Name
            };

            var battles = await BattleStore.GetBattlesAsync(battleEntityFilter, CancellationToken);
            return Mapper.Map<IEnumerable<BattleEntity>, IEnumerable<Battle>>(battles);
        }

        public Task<Result> UpdateBattleAsync(UpdateBattleCommand command)
        {
            var battle = new BattleEntity()
            {
                BattleId = command.BattleId,
                Description = command.Description,
                Name = command.Name
            };
            return BattleStore.UpdateBattleAsync(battle, CancellationToken);
        }

        public Task<Result> DeleteBattleAsync(DeleteBattleCommand command)
        {
            return BattleStore.DeleteBattleAsync(command.BattleId, CancellationToken);
        }
    }
}
