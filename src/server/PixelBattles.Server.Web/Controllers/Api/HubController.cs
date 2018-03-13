using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PixelBattles.Server.BusinessLogic.Managers;
using PixelBattles.Server.BusinessLogic.Models;
using PixelBattles.Server.Core;
using PixelBattles.Shared.DataTransfer;
using PixelBattles.Shared.DataTransfer.Api.Hub;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PixelBattles.Server.Web.Controllers.Api
{
    [Route("api")]
    public class HubController : BaseApiController
    {
        protected IHubManager HubManager { get; set; }

        public HubController(
            IHubManager hubManager,
            IMapper mapper,
            ILogger<HubController> logger) : base(
                mapper: mapper,
                logger: logger)
        {
            HubManager = hubManager ?? throw new ArgumentNullException(nameof(hubManager));
        }

        [HttpGet("hub")]
        public async Task<IActionResult> GetHubsAsync()
        {
            try
            {
                var hubs = await HubManager.GetHubsAsync();
                if (hubs == null)
                {
                    return NotFound();
                }
                var result = Mapper.Map<IEnumerable<Hub>, IEnumerable<HubDTO>>(hubs);
                return Ok(result);
            }
            catch (Exception exception)
            {
                return OnException(exception, "Error while getting hubs.");
            }
        }

        [HttpPost("hub")]
        public async Task<IActionResult> CreateHubAsync(CreateHubDTO commandDTO)
        {
            try
            {
                var command = new CreateHubCommand()
                {
                    Name = commandDTO.Name
                };
                var result = await HubManager.CreateHubAsync(command);
                var resultDTO = Mapper.Map<CreateHubResult, CreateHubResultDTO>(result);
                return OnResult(resultDTO);
            }
            catch (Exception exception)
            {
                return OnException(exception, "Error while creating hub.");
            }
        }

        [HttpDelete("hub/{hubId:guid}")]
        public async Task<IActionResult> DeleteHubAsync(Guid hubId)
        {
            try
            {
                var deleteResult = await HubManager.DeleteHubAsync(hubId);
                var result = Mapper.Map<Result, ResultDTO>(deleteResult);
                return Ok(result);
            }
            catch (Exception exception)
            {
                return OnException(exception, "Error while deleting hub.");
            }
        }
    }
}
