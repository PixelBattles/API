using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PixelBattles.Server.BusinessLogic.Managers;
using PixelBattles.Server.BusinessLogic.Models;
using PixelBattles.Shared.DataTransfer.Api.Battle;
using System;
using System.Threading.Tasks;

namespace PixelBattles.Server.Web.Controllers.Api
{
    [Route("api")]
    public class BattleController : BaseApiController
    {
        protected IBattleManager BattleManager { get; set; }

        public BattleController(
            IBattleManager battleManager,
            IMapper mapper,
            ILogger<BattleController> logger) : base(
                mapper: mapper,
                logger: logger)
        {
            BattleManager = battleManager ?? throw new ArgumentNullException(nameof(battleManager));
        }
        
        [HttpGet("battle/{battleId:guid}")]
        public async Task<IActionResult> GetBattleAsync(Guid battleId)
        {
            try
            {
                var battle = await BattleManager.GetBattleAsync(battleId);
                if (battle == null)
                {
                    return NotFound();
                }
                var result = Mapper.Map<Battle, BattleDTO>(battle);
                return Ok(result);
            }
            catch (Exception exception)
            {
                return Exception(exception, "Error while getting battles.");
            }
        }

        [HttpPost("battle")]
        public async Task<IActionResult> CreateBattleAsync(CreateBattleDTO commandDTO)
        {
            try
            {
                CreateBattleCommand command = new CreateBattleCommand()
                {
                    Name = commandDTO.Name,
                    Description = commandDTO.Description
                };
                var result = await BattleManager.CreateBattleAsync(command);
                var resultDTO = Mapper.Map<CreateBattleResult, CreateBattleResultDTO>(result);
                return Ok(resultDTO);
            }
            catch (Exception exception)
            {
                return Exception(exception, "Error while creating battle.");
            }
        }
    }
}
