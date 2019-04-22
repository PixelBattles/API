using System;
using System.Collections.Generic;

namespace PixelBattles.API.Server.BusinessLogic.Models
{
    public class CreateBattleResult : Result
    {
        public long? BattleId { get; set; }

        public CreateBattleResult(long battleId) : base(succeeded: true)
        {
            this.BattleId = battleId;
        }

        public CreateBattleResult(params Error[] errors) : base(false, errors)
        {

        }

        public CreateBattleResult(Error error) : base(false, error)
        {

        }

        public CreateBattleResult(IEnumerable<Error> errors) : base(false, errors)
        {

        }
    }
}
