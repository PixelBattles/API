using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using PixelBattles.API.Server.DataStorage.Models;

namespace PixelBattles.API.Server.DataStorage.MongoDb
{
    public class Int64IdGenerator<T> : IIdGenerator
    {
        private readonly string _idSequenceCollectionName;
        private const string SequenceFieldName = "sequenceValue";
        public Int64IdGenerator()
        {
            _idSequenceCollectionName = typeof(T).Name;
        }

        public object GenerateId(object container, object document)
        {
            var collection = (IMongoCollection<BattleEntity>)container;
            var idSequenceCollection = collection.Database.GetCollection<BsonDocument>(
                name: "Int64IdGenerator", 
                settings: new MongoCollectionSettings { WriteConcern = WriteConcern.Acknowledged });
            
            var updateResult = idSequenceCollection.FindOneAndUpdate(
                filter: Builders<BsonDocument>.Filter.Eq("_id", _idSequenceCollectionName),
                update: Builders<BsonDocument>.Update.Inc(SequenceFieldName, 1L),
                options: new FindOneAndUpdateOptions<BsonDocument, BsonDocument>()
                {
                    IsUpsert = true,
                    ReturnDocument = ReturnDocument.After
                });

            return updateResult[SequenceFieldName].AsInt64;
        }

        public bool IsEmpty(object id)
        {
            return ((id is long currentId) && (currentId == 0));
        }
    }
}