using PixelBattles.Server.Core;
using System;
using System.Collections.Generic;

namespace PixelBattles.Server.BusinessLogic.Models
{
    public class CreateGameResult : Result
    {
        public Guid GameId { get; set; }

        public CreateGameResult(Guid gameId) : base(succeeded: true)
        {
            this.GameId = gameId;
        }

        public CreateGameResult(params Error[] errors) : base(false, errors)
        {

        }

        public CreateGameResult(IEnumerable<Error> errors) : base(false, errors)
        {

        }
    }
}
