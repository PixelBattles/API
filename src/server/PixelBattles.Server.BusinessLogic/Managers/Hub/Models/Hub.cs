using PixelBattles.Server.DataStorage.Models;
using System;

namespace PixelBattles.Server.BusinessLogic.Models
{
    public class Hub
    {
        public Guid HubId { get; set; }

        public string Name { get; set; }
    }

    public partial class BusinessLogicMappingProfile
    {
        private void InitializeHub()
        {
            CreateMap<HubEntity, Hub>();
        }
    }
}
