using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace PixelBattles.Server.Web.Controllers.Api
{
    [Route("api")]
    public class UserBattlesController : BaseApiController
    {
        public UserBattlesController(
            IMapper mapper,
            ILogger<GameController> logger) : base(
                mapper: mapper,
                logger: logger)
        {
        }
    }
}
