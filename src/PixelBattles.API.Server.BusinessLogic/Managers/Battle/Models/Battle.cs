using PixelBattles.API.Server.DataStorage.Stores.Battles;
using System;

namespace PixelBattles.API.Server.BusinessLogic.Models
{
    public class Battle
    {
        public long BattleId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public BattleSettings Settings { get; set; }

        public DateTime StartDateUTC { get; set; }

        public DateTime EndDateUTC { get; set; }
    }

    public partial class BusinessLogicMappingProfile
    {
        private void InitializeBattle()
        {
            CreateMap<BattleEntity, Battle>();
        }
    }
}
