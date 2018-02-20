using PixelBattles.Server.BusinessLogic.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PixelBattles.Server.BusinessLogic.Processors
{
    public sealed class SimpleGameProcessor : GameProcessor, IGameProcessor
    {
        bool disposed = false;

        private Task updateTask; 

        private SemaphoreSlim updateSemaphore;

        private CancellationTokenSource updateCancellationTokenSource;

        private int queueLimit;

        public SimpleGameProcessor(Game game, int queueLimit = 100) : base(game)
        {
            this.queueLimit = queueLimit;

            updateCancellationTokenSource = new CancellationTokenSource();
            updateSemaphore = new SemaphoreSlim(ActionQueue.Count > queueLimit ? 1 : 0, 1);
            updateTask = Task.Run(UpdateStateAsync, updateCancellationTokenSource.Token);
        }
        
        private async Task UpdateStateAsync()
        {
            while (true)
            {
                await updateSemaphore.WaitAsync(updateCancellationTokenSource.Token);
                try
                {
                    UpdateState();
                }
                catch (Exception)
                {
                    //log here
                }
            }
        }

        public Task<ProcessUserActionResult> ProcessUserActionAsync(ProcessUserActionCommand command)
        {
            UserAction action = new UserAction()
            {
                YIndex = command.YIndex,
                XIndex = command.XIndex,
                Pixel = command.Pixel
            };

            ActionQueue.Enqueue(action);

            if (ActionQueue.Count > queueLimit)
            {
                updateSemaphore.Release();
            }

            var result = new ProcessUserActionResult();
            return Task.FromResult(result);
        }

        public Task<GameState> GetGameStateAsync()
        {
            var gameState = new GameState()
            {
                State = State,
                PendingActions = ActionQueue.ToArray()
            };
            return Task.FromResult(gameState);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                updateCancellationTokenSource.Cancel();
                updateTask.Wait();

                updateSemaphore.Dispose();
                updateCancellationTokenSource.Dispose();
                updateTask.Dispose();
            }

            disposed = true;

            base.Dispose(disposing);
        }

        public Task<GameDeltaResult> GetGameDeltaAsync(int from, int to)
        {
            throw new NotImplementedException();
        }
    }
}
