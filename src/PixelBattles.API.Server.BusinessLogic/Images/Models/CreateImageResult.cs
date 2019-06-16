using System;
using System.Collections.Generic;

namespace PixelBattles.API.Server.BusinessLogic.Images.Models
{
    public class CreateImageResult : Result
    {
        public Guid? ImageId { get; set; }

        public CreateImageResult(Guid imageId) : base(succeeded: true)
        {
            this.ImageId = imageId;
        }

        public CreateImageResult(params Error[] errors) : base(false, errors)
        {

        }

        public CreateImageResult(Error error) : base(false, error)
        {

        }

        public CreateImageResult(IEnumerable<Error> errors) : base(false, errors)
        {

        }
    }
}