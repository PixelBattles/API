using PixelBattles.Server.Core;
using System.Collections.Generic;

namespace PixelBattles.Server.BusinessLogic.Processors
{
    public class ProcessUserActionResult : Result
    {
        public UserAction UserAction { get; set; }

        public ProcessUserActionResult(UserAction userAction) : base(succeeded: true)
        {
            this.UserAction = userAction;
        }

        public ProcessUserActionResult(params Error[] errors) : base(false, errors)
        {

        }

        public ProcessUserActionResult(IEnumerable<Error> errors) : base(false, errors)
        {

        }
    }
}
