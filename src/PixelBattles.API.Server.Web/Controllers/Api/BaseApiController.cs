using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PixelBattles.API.DataTransfer;
using System;

namespace PixelBattles.API.Server.Web.Controllers.Api
{
    public abstract class BaseApiController : Controller
    {
        protected ILogger Logger { get; set; }
        protected IMapper Mapper { get; set; }

        public BaseApiController(IMapper mapper, ILogger logger)
        {
            this.Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        protected ObjectResult OnException(Exception exception, string message, params object[] args)
        {
            Logger.LogError(-1, exception, message, args);
            return StatusCode(500, Result.Failed(new Error("Unknown error", message)));
        }
        
        protected ActionResult OnResult(ResultDTO result)
        {
            if (result.Succeeded)
            {
                return Ok(result);
            }
            else
            {
                return StatusCode(400, result);
            }
        }
    }
}
