using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;

namespace PixelBattles.API.Server.DataStorage.Stores.Images
{
    public class ImageEntity
    {
        [BsonId(IdGenerator = typeof(GuidGenerator))]
        public Guid ImageId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public byte[] Data { get; set; }
    }
}