using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PixelBattles.Server.BusinessLogic.Managers;
using PixelBattles.Server.BusinessLogic.Models;
using PixelBattles.Shared.DataTransfer.Api.Battle;
using PixelBattles.Shared.DataTransfer.Api.Game;
using System;
using System.Threading.Tasks;

namespace PixelBattles.Server.Web.Controllers.Api
{
    [Route("api")]
    public class GameController : BaseApiController
    {
        protected IGameManager GameManager { get; set; }

        public GameController(
            IGameManager gameManager,
            IMapper mapper,
            ILogger<GameController> logger) : base(
                mapper: mapper,
                logger: logger)
        {
            GameManager = gameManager ?? throw new ArgumentNullException(nameof(gameManager));
        }

        [HttpGet("game/{gameId:guid}")]
        public async Task<IActionResult> GetGameAsync(Guid gameId)
        {
            try
            {
                var game = await GameManager.GetGameAsync(gameId);
                if (game == null)
                {
                    return NotFound();
                }
                var result = Mapper.Map<Game, GameDTO>(game);
                return Ok(result);
            }
            catch (Exception exception)
            {
                return OnException(exception, "Error while getting game.");
            }
        }

        [HttpGet("battle/{battleId:guid}/game")]
        public async Task<IActionResult> GetBattleGameAsync(Guid battleId)
        {
            try
            {
                var game = await GameManager.GetBattleGameAsync(battleId);
                if (game == null)
                {
                    return NotFound();
                }
                var result = Mapper.Map<Game, GameDTO>(game);
                return Ok(result);
            }
            catch (Exception exception)
            {
                return OnException(exception, "Error while getting game.");
            }
        }

        [HttpGet("game/{gameId:guid}/image")]
        public async Task<IActionResult> GetGameImageAsync(Guid gameId)
        {
            try
            {
                Game game = await GameManager.GetGameAsync(gameId);
                if (game == null)
                {
                    return NotFound();
                }
                return File(game.State, "image/png");
            }
            catch (Exception exception)
            {
                return OnException(exception, "Error while getting game image.");
            }
        }

        [HttpPost("game")]
        public async Task<IActionResult> CreateGameAsync(CreateGameDTO commandDTO)
        {
            try
            {
                var command = new CreateGameCommand()
                {
                     BattleId = commandDTO.BattleId,
                     Cooldown = commandDTO.Cooldown,
                     StartDateUTC = commandDTO.StartDateUTC,
                     EndDateUTC = commandDTO.EndDateUTC,
                     Height = commandDTO.Height,
                     Width = commandDTO.Width,
                     Name = commandDTO.Name
                };
                var result = await GameManager.CreateGameAsync(command);
                var resultDTO = Mapper.Map<CreateGameResult, CreateGameResultDTO>(result);
                return OnResult(resultDTO);
            }
            catch (Exception exception)
            {
                return OnException(exception, "Error while creating game.");
            }
        }

        [HttpPost("game/token")]
        public async Task<IActionResult> CreateGameTokenAsync(CreateGameTokenDTO commandDTO)
        {
            try
            {
                var command = new CreateGameTokenCommand()
                {
                     GameId = commandDTO.GameId
                };
                var result = await GameManager.CreateGameTokenAsync(command);
                var resultDTO = Mapper.Map<CreateGameTokenResult, CreateGameTokenResultDTO>(result);
                return OnResult(resultDTO);
            }
            catch (Exception exception)
            {
                return OnException(exception, "Error while creating game token.");
            }
        }
    }
}
