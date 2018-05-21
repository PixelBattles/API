using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;

namespace PixelBattles.Server.BusinessLogic.Managers
{
    public abstract class BaseManager
    {
        protected readonly HttpContext HttpContext;

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
    }
}
