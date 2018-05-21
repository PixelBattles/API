using MongoDB.Bson.Serialization.Attributes;
using System;

namespace PixelBattles.Server.DataStorage.Models
{
    public class BattleEntity
    {
        [BsonId]
        public Guid BattleId { get; set; }
        
        public string Name { get; set; }

        public string Description { get; set; }
    }
}