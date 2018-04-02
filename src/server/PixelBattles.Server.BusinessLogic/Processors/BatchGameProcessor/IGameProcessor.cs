using System;
using System.Threading.Tasks;

namespace PixelBattles.Server.BusinessLogic.Processors
{
    public interface IGameProcessor : IDisposable
    {
        Task<ProcessUserActionResult> ProcessUserActionAsync(ProcessUserActionCommand command);

        Task<GameState> GetGameStateAsync();

        Task<GameDeltaResult> GetGameDeltaAsync(int from, int to);
    }
}
