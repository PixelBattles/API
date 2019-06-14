using MongoDB.Bson.Serialization.Attributes;
using PixelBattles.API.Server.DataStorage.MongoDb;
using System;

namespace PixelBattles.API.Server.DataStorage.Stores.Battles
{
    public class BattleEntity
    {
        [BsonId(IdGenerator = typeof(Int64IdGenerator<BattleEntity>))]
        public long BattleId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public BattleSettingsEntity Settings { get; set; }

        public DateTime StartDateUTC { get; set; }

        public DateTime EndDateUTC { get; set; }
    }
}