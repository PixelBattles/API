using System;

namespace PixelBattles.Shared.DataTransfer.Api.Hub
{
    public class CreateHubResultDTO : ResultDTO
    {
        public Guid? HubId { get; set; }
    }
}