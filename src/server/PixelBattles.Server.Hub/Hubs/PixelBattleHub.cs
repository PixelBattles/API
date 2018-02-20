using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using PixelBattles.Server.BusinessLogic.Processors;
using PixelBattles.Shared.DataTransfer.Hub;
using SixLabors.ImageSharp;
using System;
using System.Threading.Tasks;

namespace PixelBattles.Server.Hub
{
    [Authorize]
    public class PixelBattleHub : Microsoft.AspNetCore.SignalR.Hub
    {
        protected PixelBattleHubContext PixelBattleHubContext { get; set; }

        protected IMapper Mapper { get; set; }

        public PixelBattleHub(
            PixelBattleHubContext pixelBattleHubContext,
            IMapper mapper)
        {
            PixelBattleHubContext = pixelBattleHubContext;
            Mapper = mapper;
        }

        public async override Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
        }

        public async override Task OnDisconnectedAsync(Exception exception)
        {
            await base.OnDisconnectedAsync(exception);
        }

        public async Task<bool> ConnectGame(Guid gameId)
        {
            if (PixelBattleHubContext.ContainsGame(gameId))
            {
                await Groups.AddAsync(Context.ConnectionId, gameId.ToString());
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> DisconnectGame(Guid gameId)
        {
            if (PixelBattleHubContext.ContainsGame(gameId))
            {
                await Groups.RemoveAsync(Context.ConnectionId, gameId.ToString());
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<GameStateDTO> GetGameState(Guid gameId)
        {
            IGameProcessor gameProcessor = PixelBattleHubContext.GetGame(gameId);
            if (gameProcessor != null)
            {
                var gameState = await gameProcessor.GetGameStateAsync();
                var gameStateResult = Mapper.Map<GameState, GameStateDTO>(gameState);
                return gameStateResult;
            }
            return null;
        }

        public async Task<ProcessActionResultDTO> ProcessAction(ProcessActionDTO commandDTO)
        {
            IGameProcessor gameProcessor = PixelBattleHubContext.GetGame(commandDTO.GameId);
            if (gameProcessor != null)
            {
                ProcessUserActionCommand command = new ProcessUserActionCommand()
                {
                    GameId = commandDTO.GameId,
                    Pixel = new Rgba32(commandDTO.Pixel[0], commandDTO.Pixel[1], commandDTO.Pixel[2]),
                    XIndex = commandDTO.XIndex,
                    YIndex = commandDTO.YIndex
                };
                var result = await gameProcessor.ProcessUserActionAsync(command);
                var resultDTO = Mapper.Map<ProcessUserActionResult, ProcessActionResultDTO>(result);
                if (resultDTO.Succeeded)
                {
                    await Clients.Group(command.GameId.ToString()).InvokeAsync("OnAction", new object[] { resultDTO.userAction });
                }                
                return resultDTO;
            }
            return null;
        }

        public async Task<GameDeltaResultDTO> GetGameDelta(Guid gameId, int from, int to)
        {
            IGameProcessor gameProcessor = PixelBattleHubContext.GetGame(gameId);
            if (gameProcessor != null)
            {
                var gameDelta = await gameProcessor.GetGameDeltaAsync(from, to);
                var gameDeltaResult = Mapper.Map<GameDeltaResult, GameDeltaResultDTO>(gameDelta);
                return gameDeltaResult;
            }
            return null;
        }
    }
}
