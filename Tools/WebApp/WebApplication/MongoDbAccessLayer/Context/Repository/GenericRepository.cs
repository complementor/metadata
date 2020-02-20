using MongoDB.Bson;
using MongoDB.Driver;
using MongoDbAccessLayer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MongoDbAccessLayer.Context.Repository
{
    public abstract class GenericRepository  : IGenericRepository 
    {
        protected readonly IMongoVideoDbContext _mongoContext;
        protected IMongoCollection<Generic> _dbCollection;

        protected GenericRepository(IMongoVideoDbContext context)
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
    }
}