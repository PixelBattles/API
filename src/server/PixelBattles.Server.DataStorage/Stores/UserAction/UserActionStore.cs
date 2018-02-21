using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using PixelBattles.Server.Core;
using PixelBattles.Server.DataStorage.Models;
using System;

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

        public async Task<Result> CreateBatchAsync(IEnumerable<UserActionEntity> userActions, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (userActions == null)
            {
                throw new ArgumentNullException(nameof(userActions));
            }
            DbSet.AddRange(userActions);
            await SaveChanges(cancellationToken);
            return Result.Success;
        }
    }
}
