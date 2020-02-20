using MongoDB.Driver;
using Microsoft.Extensions.Options;
using System;

namespace MongoDbAccessLayer.Context
{
    public class MongoVideoDbContext : IMongoVideoDbContext
    {
        private IMongoDatabase _db { get; }
        private MongoClient _mongoClient { get; }
        public MongoVideoDbContext(IOptions<MongoSettings> configuration)
        {

            if (configuration == null ) throw new ArgumentNullException(nameof(configuration));
            _mongoClient = new MongoClient(configuration.Value.Connection);
            _db = _mongoClient.GetDatabase(configuration.Value.DatabaseName);
        }
        public IMongoCollection<T> GetCollection<T>(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return null;

            return _db.GetCollection<T>(name);
        }
    }
}