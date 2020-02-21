using MongoDB.Bson;
using MongoDB.Driver;
using MongoDbAccessLayer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MongoDbAccessLayer.Context.Repository
{
    public class GenericRepository : IGenericRepository
    {
        public readonly IMongoVideoDbContext _mongoContext;
        public IMongoCollection<Generic> _dbCollection;

        public GenericRepository(IMongoVideoDbContext context)
        {
            _mongoContext = context;
            _dbCollection = _mongoContext.GetCollection<Generic>(typeof(Generic).Name);
        }

        public async Task<Generic> Get(string id)
        {
            //ex. 5dc1039a1521eaa36835e541

            var objectId = new ObjectId(id);

            FilterDefinition<Generic> filter = Builders<Generic>.Filter.Eq("_id", objectId);

            _dbCollection = _mongoContext.GetCollection<Generic>(typeof(Generic).Name);

            return await _dbCollection.FindAsync(filter).Result.FirstOrDefaultAsync();

        }
        public async Task<IEnumerable<Generic>> GetAll()
        {
            var all = await _dbCollection.FindAsync(Builders<Generic>.Filter.Empty);
            return await all.ToListAsync();
        }

        public async Task<Generic> GetById(string guid)
        {
            //ex. 5dc1039a1521eaa36835e541
            //maybe use projects here to only needed
            FilterDefinition<Generic> filter = Builders<Generic>.Filter.Eq("Id", guid);

            _dbCollection = _mongoContext.GetCollection<Generic>(typeof(Generic).Name);

            return await _dbCollection.FindAsync(filter).Result.FirstOrDefaultAsync();
        }
    }
}