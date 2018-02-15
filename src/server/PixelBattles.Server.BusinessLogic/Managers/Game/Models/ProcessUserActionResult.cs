using PixelBattles.Server.Core;
using System.Collections.Generic;

namespace PixelBattles.Server.BusinessLogic.Models
{
    public class ProcessUserActionResult : Result
    {
        public ProcessUserActionResult() : base(succeeded: true)
        {

        }

        public ProcessUserActionResult(params Error[] errors) : base(false, errors)
        {

        }

        public ProcessUserActionResult(IEnumerable<Error> errors) : base(false, errors)
        {

        }
    }
}
