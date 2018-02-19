using AutoMapper;
using PixelBattles.Server.BusinessLogic.Processors;
using PixelBattles.Shared.DataTransfer.Hub;
using SixLabors.ImageSharp;
using System;
using System.Threading.Tasks;

namespace PixelBattles.Server.Hub
{
    public class PixelBattleHub : Microsoft.AspNetCore.SignalR.Hub<IHubClient>
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
            if (PixelBattleHubContext.Games.ContainsKey(gameId))
            {
                await Groups.AddAsync(Context.ConnectionId, gameId.ToString());
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<GameStateDTO> GetGameState(Guid gameId)
        {
            if (PixelBattleHubContext.Games.TryGetValue(gameId, out IGameProcessor gameProcessor))
            {
                var gameState = await gameProcessor.GetGameStateAsync();
                var gameStateResult = Mapper.Map<GameState, GameStateDTO>(gameState);
                return gameStateResult;
            }
            return null;
        }

        public async Task<ProcessActionResultDTO> ProcessAction(ProcessActionDTO commandDTO)
        {
            if (PixelBattleHubContext.Games.TryGetValue(commandDTO.GameId, out IGameProcessor gameProcessor))
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
                return resultDTO;
            }
            return null;
        }

        public async Task<bool> DisconnectGame(Guid gameId)
        {
            if (PixelBattleHubContext.Games.ContainsKey(gameId))
            {
                await Groups.RemoveAsync(Context.ConnectionId, gameId.ToString());
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
