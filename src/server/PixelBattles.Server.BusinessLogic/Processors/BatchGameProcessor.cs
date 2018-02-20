using PixelBattles.Server.BusinessLogic.Models;
using System;
using System.Threading.Tasks;

namespace PixelBattles.Server.BusinessLogic.Processors
{
    public sealed class BatchGameProcessor : IGameProcessor
    {
        private Game game;

        public BatchGameProcessor(Game game)
        {
            this.game = game;
        }



        public void Dispose()
        {
        }
        
        public Task<GameDeltaResult> GetGameDeltaAsync(int from, int to)
        {
            throw new NotImplementedException();
        }

        public Task<GameState> GetGameStateAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ProcessUserActionResult> ProcessUserActionAsync(ProcessUserActionCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
