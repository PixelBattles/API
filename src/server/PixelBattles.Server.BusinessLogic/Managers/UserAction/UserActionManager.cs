using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace PixelBattles.Server.BusinessLogic.Managers
{
    [Register(typeof(IUserActionManager))]
    public class UserActionManager : BaseManager, IUserActionManager
    {
        public UserActionManager(
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
