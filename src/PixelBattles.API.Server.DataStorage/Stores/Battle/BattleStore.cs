using MongoDB.Driver;
using PixelBattles.API.Server.DataStorage.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PixelBattles.API.Server.DataStorage.Stores
{
    public class BattleStore : IBattleStore
    {
        private IMongoCollection<BattleEntity> battleCollection;

        public BattleStore(IMongoCollection<BattleEntity> battleCollection)
        {
            this.battleCollection = battleCollection ?? throw new ArgumentNullException(nameof(battleCollection));
        }

        public async Task<BattleEntity> GetBattleAsync(Guid battleId, CancellationToken cancellationToken = default(CancellationToken))
        {
            var result = await battleCollection.FindAsync(t => t.BattleId == battleId, null, cancellationToken);
            return await result.SingleAsync(cancellationToken);
        }

        public async Task<Result> CreateBattleAsync(BattleEntity battleEntity, CancellationToken cancellationToken = default(CancellationToken))
        {
            await battleCollection.InsertOneAsync(battleEntity, null, cancellationToken);
            return Result.Success;
        }

        public async Task<Result> UpdateBattleAsync(BattleEntity battleEntity, CancellationToken cancellationToken = default(CancellationToken))
        {
            var result = await battleCollection.ReplaceOneAsync(t => t.BattleId == battleEntity.BattleId, battleEntity, new UpdateOptions { IsUpsert = false }, cancellationToken);
            if (result.MatchedCount == 0)
            {
                return Result.Failed(new Error("Battle not found", "Battle not found"));
            }
            return Result.Success;
        }

        public async Task<IEnumerable<BattleEntity>> GetBattlesAsync(BattleEntityFilter battleEntityFilter, CancellationToken cancellationToken = default(CancellationToken))
        {
            var filterBuilder = Builders<BattleEntity>.Filter;
            FilterDefinition<BattleEntity> filter = null;
            if (!String.IsNullOrEmpty(battleEntityFilter.Name))
            {
                filter = filterBuilder.Text(battleEntityFilter.Name);
            }
            
            var result = await battleCollection.FindAsync(
                filter ?? FilterDefinition<BattleEntity>.Empty, 
                new FindOptions<BattleEntity, BattleEntity>
                {
                }, 
                cancellationToken);

            return await result.ToListAsync();
        }

        public async Task<Result> DeleteBattleAsync(Guid battleId, CancellationToken cancellationToken = default(CancellationToken))
        {
            await battleCollection.DeleteOneAsync(t => t.BattleId == battleId, cancellationToken);
            return Result.Success;
        }
    }
}
