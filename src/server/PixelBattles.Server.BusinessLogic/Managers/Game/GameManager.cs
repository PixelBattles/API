using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PixelBattles.Server.DataStorage.Stores;

namespace PixelBattles.Server.BusinessLogic.Managers
{
    [Register(typeof(IGameManager))]
    public class GameManager : BaseManager, IGameManager
    {
        public GameManager(
            IHttpContextAccessor contextAccessor,
            ErrorDescriber errorDescriber,
            IMapper mapper,
            ILogger<GameManager> logger)
            : base(
                  contextAccessor: contextAccessor,
                  errorDescriber: errorDescriber,
                  mapper: mapper,
                  logger: logger)
        {
        }

        protected override void DisposeStores()
        {
        }
    }
}
