using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PixelBattles.Server.Core;
using System;

namespace PixelBattles.Server.Web.Controllers.Api
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
        
        protected ActionResult OnResult(Result result)
        {
            if (result.Succeeded)
            {
                return Ok(result);
            }
            else
            {
                return StatusCode(500, result);
            }
        }
    }
}
