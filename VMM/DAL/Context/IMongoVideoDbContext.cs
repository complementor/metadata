using MongoDB.Driver;

namespace MongoDbAccessLayer.Context
{
    public interface IMongoVideoDbContext 
    {
        public IMongoCollection<T> GetCollection<T>(string name);
    }
}