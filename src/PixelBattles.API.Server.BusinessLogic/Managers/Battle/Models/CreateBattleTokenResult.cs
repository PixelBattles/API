using System.Collections.Generic;

namespace PixelBattles.API.Server.BusinessLogic.Models
{
    public class CreateBattleTokenResult : Result
    {
        public string Token { get; set; }

        public CreateBattleTokenResult(string token) : base(succeeded: true)
        {
            this.Token = token;
        }

        public CreateBattleTokenResult(params Error[] errors) : base(false, errors)
        {

        }

        public CreateBattleTokenResult(IEnumerable<Error> errors) : base(false, errors)
        {

        }
    }
}
