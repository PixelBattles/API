using System;
using System.Collections.Generic;

namespace PixelBattles.API.Server.BusinessLogic.Models
{
    public class CreateBattleResult : Result
    {
        public Guid? BattleId { get; set; }

        public CreateBattleResult(Guid battleId) : base(succeeded: true)
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
