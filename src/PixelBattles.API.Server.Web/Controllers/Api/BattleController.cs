﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PixelBattles.API.DataTransfer;
using PixelBattles.API.DataTransfer.Battles;
using PixelBattles.API.Server.BusinessLogic.Battles;
using PixelBattles.API.Server.BusinessLogic.Battles.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PixelBattles.API.Server.Web.Controllers.Api
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
        
        [HttpGet("battle/{battleId:long}")]
        public async Task<IActionResult> GetBattleAsync(long battleId)
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
                return OnException(exception, "Error while getting battle.");
            }
        }

        [HttpGet("battle")]
        public async Task<IActionResult> GetBattlesAsync(BattleFilterDTO battleFilterDTO)
        {
            try
            {
                var battleFilter = new BattleFilter()
                {
                    Name = battleFilterDTO.Name
                };

                var battles = await BattleManager.GetBattlesAsync(battleFilter);
                if (battles == null)
                {
                    return NotFound();
                }

                var battlesResult = Mapper.Map<IEnumerable<Battle>, IEnumerable<BattleDTO>>(battles);
                return Ok(battlesResult);
            }
            catch (Exception exception)
            {
                return OnException(exception, "Error while getting battles.");
            }
        }

        [HttpPost("battle")]
        public async Task<IActionResult> CreateBattleAsync([FromBody] CreateBattleDTO commandDTO)
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
                return OnResult(resultDTO);
            }
            catch (Exception exception)
            {
                return OnException(exception, "Error while creating battle.");
            }
        }

        [HttpDelete("battle/{battleId:long}")]
        public async Task<IActionResult> DeleteBattleAsync(long battleId)
        {
            try
            {
                DeleteBattleCommand command = new DeleteBattleCommand()
                {
                    BattleId = battleId
                };
                var result = await BattleManager.DeleteBattleAsync(command);
                var resultDTO = Mapper.Map<Result, ResultDTO>(result);
                return OnResult(resultDTO);
            }
            catch (Exception exception)
            {
                return OnException(exception, "Error while deleting battle.");
            }
        }

        [HttpPut("battle/{battleId:long}")]
        public async Task<IActionResult> UpdateBattleAsync(long battleId, [FromBody] UpdateBattleDTO commandDTO)
        {
            try
            {
                var command = new UpdateBattleCommand()
                {
                    BattleId = battleId,
                    Name = commandDTO.Name,
                    Description = commandDTO.Description
                };
                var result = await BattleManager.UpdateBattleAsync(command);
                var resultDTO = Mapper.Map<Result, ResultDTO>(result);
                return OnResult(resultDTO);
            }
            catch (Exception exception)
            {
                return OnException(exception, "Error while updating battle.");
            }
        }

        [HttpPost("battle/{battleId:long}/token")]
        public async Task<IActionResult> CreateBattleTokenAsync(long battleId)
        {
            try
            {
                var command = new CreateBattleTokenCommand()
                {
                    BattleId = battleId,
                    UserId = Guid.Empty
                };
                var result = await BattleManager.CreateBattleTokenAsync(command);
                var resultDTO = Mapper.Map<CreateBattleTokenResult, CreateBattleTokenResultDTO>(result);
                return OnResult(resultDTO);
            }
            catch (Exception exception)
            {
                return OnException(exception, "Error while creating battle token.");
            }
        }

        [HttpPut("battle/{battleId:long}/image")]
        public async Task<IActionResult> UpdateBattleImageAsync(long battleId, [FromBody] UpdateBattleImageDTO commandDTO)
        {
            try
            {
                var command = new UpdateBattleImageCommand()
                {
                    BattleId = battleId,
                    ImageId = commandDTO.ImageId
                };
                var result = await BattleManager.UpdateBattleImageAsync(command);
                var resultDTO = Mapper.Map<Result, ResultDTO>(result);
                return OnResult(resultDTO);
            }
            catch (Exception exception)
            {
                return OnException(exception, "Error while updating battle image.");
            }
        }
    }
}