using PixelBattles.Server.Core;
using System.Collections.Generic;

namespace PixelBattles.Server.BusinessLogic.Models
{
    public class CreateGameTokenResult : Result
    {
        public string Token { get; set; }

        public CreateGameTokenResult(string token) : base(succeeded: true)
        {
            this.Token = token;
        }

        public CreateGameTokenResult(params Error[] errors) : base(false, errors)
        {

        }

        public CreateGameTokenResult(IEnumerable<Error> errors) : base(false, errors)
        {

        }
    }
}
