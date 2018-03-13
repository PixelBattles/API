using PixelBattles.Server.Core;
using System;
using System.Collections.Generic;

namespace PixelBattles.Server.BusinessLogic.Models
{
    public class CreateHubResult : Result
    {
        public Guid? HubId { get; set; }

        public CreateHubResult(Guid hubId) : base(succeeded: true)
        {
            this.HubId = hubId;
        }

        public CreateHubResult(params Error[] errors) : base(false, errors)
        {

        }

        public CreateHubResult(Error error) : base(false, error)
        {

        }

        public CreateHubResult(IEnumerable<Error> errors) : base(false, errors)
        {

        }
    }
}
