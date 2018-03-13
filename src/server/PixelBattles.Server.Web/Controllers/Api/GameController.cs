using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PixelBattles.Server.BusinessLogic.Managers;
using PixelBattles.Server.BusinessLogic.Models;
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
                Game game = await GameManager.GetGameAsync(gameId);
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
    }
}
