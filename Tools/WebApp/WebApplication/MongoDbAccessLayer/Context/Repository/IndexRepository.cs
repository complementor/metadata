using MongoDB.Bson;
using MongoDB.Driver;
using MongoDbAccessLayer.DomainModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MongoDbAccessLayer.Context.Repository
{
    public class IndexRepository : IIndexRepository
    {
        public readonly IMongoVideoDbContext _mongoContext;
        public IMongoCollection<IndexModel> _dbCollection;

        public IndexRepository(IMongoVideoDbContext context)
        {
            _mongoContext = context;
            _dbCollection = _mongoContext.GetCollection<IndexModel>(typeof(IndexModel).Name);
        }

        public async Task<IndexModel> GetById(string guid)
        {
            //ex. guid
            FilterDefinition<IndexModel> filter = Builders<IndexModel>.Filter.Eq("Id", guid);

            _dbCollection = _mongoContext.GetCollection<IndexModel>(typeof(IndexModel).Name);

            return await _dbCollection.FindAsync(filter).Result.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<IndexModel>> GetAll()
        {
            var all = await _dbCollection.FindAsync(Builders<IndexModel>.Filter.Empty);
            return await all.ToListAsync();
        }

        public async Task<IndexModel> Get(string id)
        {
            //ex. 5dc1039a1521eaa36835e541

            var objectId = new ObjectId(id);

            FilterDefinition<IndexModel> filter = Builders<IndexModel>.Filter.Eq("_id", objectId);

            _dbCollection = _mongoContext.GetCollection<IndexModel>(typeof(IndexModel).Name);

            return await _dbCollection.FindAsync(filter).Result.FirstOrDefaultAsync();
        }
    }
}