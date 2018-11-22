using MongoDB.Bson.Serialization.Attributes;
using System;

namespace PixelBattles.API.Server.DataStorage.Models
{
    public class BattleEntity
    {
        [BsonId]
        public Guid BattleId { get; set; }
        
        public string Name { get; set; }

        public string Description { get; set; }

        public BattleSettingsEntity Settings { get; set; }

        public DateTime StartDateUTC { get; set; }

        public DateTime EndDateUTC { get; set; }
    }
}