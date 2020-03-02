using MongoDB.Driver;
using Microsoft.Extensions.Options;
using System;
using MongoDB.Bson;

namespace MongoDbAccessLayer.Context
{
    public class MongoVideoDbContext : IMongoVideoDbContext
    {
        private IMongoDatabase _db { get; }
        private MongoClient _mongoClient { get; }

        public MongoVideoDbContext(IOptions<MongoSettings> configuration)
        {

            if (configuration == null) throw new ArgumentNullException(nameof(configuration));
            _mongoClient = new MongoClient(configuration.Value.Connection);
            _db = _mongoClient.GetDatabase(configuration.Value.DatabaseName);

        }
        public IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            if (string.IsNullOrWhiteSpace(collectionName)) throw new ArgumentNullException(nameof(collectionName));

            return _db.ListCollectionNames(new ListCollectionNamesOptions { Filter = new BsonDocument("name", collectionName) }).Any() ?
                _db.GetCollection<T>(collectionName) : throw new ArgumentNullException(nameof(collectionName));
        }
    }
}