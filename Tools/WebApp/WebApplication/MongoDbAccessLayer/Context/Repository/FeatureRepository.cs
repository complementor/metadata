using MongoDB.Bson;
using MongoDB.Driver;
using MongoDbAccessLayer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MongoDbAccessLayer.Context.Repository
{
    public class FeatureRepository : IFeatureRepository
    {
        public readonly IMongoVideoDbContext _mongoContext;
        public IMongoCollection<Features> _dbCollection;

        public FeatureRepository(IMongoVideoDbContext context)
        {
            _mongoContext = context;
            _dbCollection = _mongoContext.GetCollection<Features>(typeof(Features).Name);
        }

        public async Task<Features> GetById(string guid)
        {
            //ex. guid
            FilterDefinition<Features> filter = Builders<Features>.Filter.Eq("Id", guid);

            _dbCollection = _mongoContext.GetCollection<Features>(typeof(Features).Name);

            return await _dbCollection.FindAsync(filter).Result.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Features>> GetAll()
        {
            var all = await _dbCollection.FindAsync(Builders<Features>.Filter.Empty);
            return await all.ToListAsync();
        }

        public async Task<Features> Get(string id)
        {
            //ex. 5dc1039a1521eaa36835e541

            var objectId = new ObjectId(id);

            FilterDefinition<Features> filter = Builders<Features>.Filter.Eq("_id", objectId);

            _dbCollection = _mongoContext.GetCollection<Features>(typeof(Features).Name);

            return await _dbCollection.FindAsync(filter).Result.FirstOrDefaultAsync();
        }
    }
}