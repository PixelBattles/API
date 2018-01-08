using Microsoft.EntityFrameworkCore;
using PixelBattles.Server.Core;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PixelBattles.Server.DataStorage.Stores
{
    public abstract class BaseStore<TEntity> : IStore<TEntity>, IDisposable where TEntity : class
    {
        public BaseStore(PixelBattlesDbContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
            DbSet = context.Set<TEntity>();
        }

        public DbContext Context { get; private set; }

        public DbSet<TEntity> DbSet { get; private set; }

        protected bool _disposed;

        protected void ThrowIfDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }
        }

        public void Dispose()
        {
            _disposed = true;
        }

        public bool AutoSaveChanges { get; set; } = true;

        protected Task SaveChanges(CancellationToken cancellationToken)
        {
            return AutoSaveChanges ? Context.SaveChangesAsync(cancellationToken) : Task.FromResult(0);
        }

        public virtual async void SaveChangesAsync(CancellationToken cancellationToken)
        {
            await Context.SaveChangesAsync(cancellationToken);
        }

        public virtual IQueryable<TEntity> Entities
        {
            get { return Context.Set<TEntity>(); }
        }

        public virtual async Task<Result> CreateAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (entity == null)
            {
                throw new ArgumentNullException(typeof(TEntity).Name);
            }
            DbSet.Add(entity);
            await SaveChanges(cancellationToken);
            return Result.Success;
        }

        public virtual async Task<Result> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (entity == null)
            {
                throw new ArgumentNullException(typeof(TEntity).Name);
            }
            DbSet.Remove(entity);
            try
            {
                await SaveChanges(cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                return Result.Failed(new Error("Concurrency failure", "Optimistic concurrency failure, object has been modified."));
            }
            return Result.Success;
        }

        public virtual async Task<Result> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (entity == null)
            {
                throw new ArgumentNullException(typeof(TEntity).Name);
            }
            DbSet.Attach(entity);
            DbSet.Update(entity);
            try
            {
                await SaveChanges(cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                return Result.Failed(new Error("Concurrency failure", "Optimistic concurrency failure, object has been modified."));
            }
            return Result.Success;
        }
    }
}
