using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;

namespace PixelBattles.Server.BusinessLogic.Managers
{
    public abstract class BaseManager : IDisposable
    {
        protected readonly HttpContext HttpContext;

        private bool disposed;

        protected CancellationToken CancellationToken => HttpContext?.RequestAborted ?? CancellationToken.None;

        protected ErrorDescriber ErrorDescriber { get; set; }

        protected internal ILogger Logger { get; set; }

        protected IMapper Mapper { get; set; }

        public BaseManager(
            IHttpContextAccessor contextAccessor,
            ErrorDescriber errorDescriber,
            IMapper mapper,
            ILogger logger)
        {
            this.HttpContext = contextAccessor?.HttpContext;
            this.ErrorDescriber = errorDescriber;
            this.Logger = logger;
            this.Mapper = mapper;
        }

        protected abstract void DisposeStores();

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                DisposeStores();
                // Free any other managed objects here.
                //
            }

            // Free any unmanaged objects here.
            //
            disposed = true;
        }

        protected void ThrowIfDisposed()
        {
            if (disposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
