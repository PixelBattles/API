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
    [Register(typeof(IHubManager))]
    public class HubManager : BaseManager, IHubManager
    {
        protected IHubStore HubStore { get; set; }

        public HubManager(
            IHubStore hubStore,
            IHttpContextAccessor contextAccessor,
            ErrorDescriber errorDescriber,
            IMapper mapper,
            ILogger<HubManager> logger)
            : base(
                  contextAccessor: contextAccessor,
                  errorDescriber: errorDescriber,
                  mapper: mapper,
                  logger: logger)
        {
            HubStore = hubStore ?? throw new ArgumentNullException(nameof(hubStore));
        }

        public async Task<Hub> GetHubAsync(Guid hubId)
        {
            ThrowIfDisposed();
            var hub = await HubStore.GetHubAsync(hubId, CancellationToken);
            return Mapper.Map<HubEntity, Hub>(hub);
        }

        protected override void DisposeStores()
        {
            HubStore.Dispose();
        }
    }
}
