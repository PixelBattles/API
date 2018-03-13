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

        public async Task<IEnumerable<Hub>> GetHubsAsync()
        {
            ThrowIfDisposed();
            var hubs = await HubStore.GetHubsAsync(CancellationToken);
            return Mapper.Map<IEnumerable<HubEntity>, IEnumerable<Hub>>(hubs);
        }

        public async Task<CreateHubResult> CreateHubAsync(CreateHubCommand command)
        {
            ThrowIfDisposed();

            if (String.IsNullOrWhiteSpace(command.Name))
            {
                return new CreateHubResult(new Error("Empty name", "Name can't be empty"));
            }

            var existingHub = await HubStore.GetHubAsync(command.Name, CancellationToken);
            if (existingHub != null)
            {
                return new CreateHubResult(new Error("Name duplication", "Hub with the same name is already exist"));
            }

            var hub = new HubEntity()
            {
                Name = command.Name
            };

            var result = await HubStore.CreateAsync(hub, CancellationToken);
            if (result.Succeeded)
            {
                return new CreateHubResult(hub.HubId);
            }
            else
            {
                return new CreateHubResult(result.Errors);
            }
        }

        public async Task<Result> DeleteHubAsync(Guid hubId)
        {
            ThrowIfDisposed();

            var hub = await HubStore.GetHubAsync(hubId, CancellationToken);

            if (hub == null)
            {
                return Result.Success;
            }

            var result = await HubStore.DeleteAsync(hub, CancellationToken);
            return result;
        }

        protected override void DisposeStores()
        {
            HubStore.Dispose();
        }
    }
}
