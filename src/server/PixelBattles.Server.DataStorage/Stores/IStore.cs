using Microsoft.EntityFrameworkCore;
using PixelBattles.Server.Core;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PixelBattles.Server.DataStorage.Stores
{
    public interface IStore<TEntity> : IDisposable where TEntity : class
    {
        IQueryable<TEntity> Entities { get; }
        DbContext Context { get; }
        bool AutoSaveChanges { get; set; }
        void SaveChangesAsync(CancellationToken cancellationToken);
        Task<Result> CreateAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken));
        Task<Result> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken));
        Task<Result> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken));
    }
}
