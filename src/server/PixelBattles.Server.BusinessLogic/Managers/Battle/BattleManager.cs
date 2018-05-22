using AutoMapper;
using Microsoft.AspNetCore.Http;
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
    public class BattleManager : BaseManager, IBattleManager
    {
        protected IBattleStore BattleStore { get; set; }
        protected IBattleTokenGenerator BattleTokenGenerator { get; set; }

        public BattleManager(
            IBattleStore battleStore,
            IBattleTokenGenerator battleTokenGenerator,
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
            BattleTokenGenerator = battleTokenGenerator ?? throw new ArgumentNullException(nameof(battleTokenGenerator));
        }

        public async Task<Battle> GetBattleAsync(Guid battleId)
        {
            var battle = await BattleStore.GetBattleAsync(battleId, CancellationToken);
            return Mapper.Map<BattleEntity, Battle>(battle);
        }
        
        public async Task<CreateBattleResult> CreateBattleAsync(CreateBattleCommand command)
        {
            if (String.IsNullOrWhiteSpace(command.Name))
            {
                return new CreateBattleResult(new Error("Empty name", "Name can't be empty"));
            }

            var battle = new BattleEntity()
            {
                Description = command.Description,
                Name = command.Name,
                Settings = new BattleSettingsEntity
                {
                    CenterX = 0,
                    CenterY = 0,
                    Cooldown = 0,
                    ChunkHeight = 100,
                    ChunkWidth = 100,
                    MaxHeightIndex = 99,
                    MaxWidthIndex = 99,
                    MinHeightIndex = -100,
                    MinWidthIndex = -100
                },
                StartDateUTC = DateTime.UtcNow,
                EndDateUTC = DateTime.UtcNow.AddDays(100)
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

        public async Task<Result> UpdateBattleAsync(UpdateBattleCommand command)
        {
            var battle = await BattleStore.GetBattleAsync(command.BattleId);
            if (battle == null)
            {
                return Result.Failed(new Error("Battle not found", "Battle not found"));
            }
            battle.Description = command.Description;
            battle.Name = command.Name;
            return await BattleStore.UpdateBattleAsync(battle, CancellationToken);
        }

        public Task<Result> DeleteBattleAsync(DeleteBattleCommand command)
        {
            return BattleStore.DeleteBattleAsync(command.BattleId, CancellationToken);
        }

        public async Task<CreateBattleTokenResult> CreateBattleTokenAsync(CreateBattleTokenCommand command)
        {
            var battle = await BattleStore.GetBattleAsync(command.BattleId, CancellationToken);

            if (battle == null)
            {
                return new CreateBattleTokenResult(new Error("Battle not found", "Battle not found"));
            }

            string token = BattleTokenGenerator.GenerateToken(command.BattleId, command.UserId);

            return new CreateBattleTokenResult(token);
        }
    }
}
