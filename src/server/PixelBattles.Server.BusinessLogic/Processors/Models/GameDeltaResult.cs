using PixelBattles.Server.Core;
using System.Collections.Generic;

namespace PixelBattles.Server.BusinessLogic.Processors
{
    public class GameDeltaResult : Result
    {
        public IEnumerable<UserAction> UserActions { get; set; }

        public GameDeltaResult(IEnumerable<UserAction> userActions) : base(succeeded: true)
        {
            this.UserActions = userActions;
        }

        public GameDeltaResult(params Error[] errors) : base(false, errors)
        {

        }

        public GameDeltaResult(IEnumerable<Error> errors) : base(false, errors)
        {

        }
    }
}
