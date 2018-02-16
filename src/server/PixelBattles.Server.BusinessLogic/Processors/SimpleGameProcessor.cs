using PixelBattles.Server.BusinessLogic.Models;

namespace PixelBattles.Server.BusinessLogic.Processors
{
    public class SimpleGameProcessor : GameProcessor, IGameProcessor
    {
        public SimpleGameProcessor(Game game) : base(game)
        {

        }
    }
}
