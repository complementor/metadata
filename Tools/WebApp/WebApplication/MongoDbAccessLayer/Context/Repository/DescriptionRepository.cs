using MongoDB.Bson;
using MongoDB.Driver;
using MongoDbAccessLayer.DomainModels;
using MongoDbAccessLayer.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDbAccessLayer.Context.Repository
{
    public class DescriptionRepository : IDescriptionRepository
    {
        public readonly IMongoVideoDbContext _mongoContext;
        public IMongoCollection<DescriptionModel> _dbCollection;

        public DescriptionRepository(IMongoVideoDbContext context)
        {
            _mongoContext = context;
            _dbCollection = _mongoContext.GetCollection<DescriptionModel>("description");
        }

        public async Task<DescriptionModel> Get(string id)
        {
            //ex. ObjectId("5e514803fa0df9b9f548f02c);

            var objectId = new ObjectId(id);

            FilterDefinition<DescriptionModel> filter = Builders<DescriptionModel>.Filter.Eq("_id", objectId);

            _dbCollection = _mongoContext.GetCollection<DescriptionModel>(typeof(DescriptionModel).Name);

            return await _dbCollection.FindAsync(filter).Result.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<DescriptionModel>> GetAll()
        {
            var all = await _dbCollection.FindAsync(Builders<DescriptionModel>.Filter.Empty);
            return await all.ToListAsync();
        }

        public async Task<DescriptionModel> GetById(string guid)
        {
            //ex. 5dc1039a1521eaa36835e541
            FilterDefinition<DescriptionModel> filter = Builders<DescriptionModel>.Filter.Eq("Id", guid);

            _dbCollection = _mongoContext.GetCollection<DescriptionModel>(typeof(DescriptionModel).Name);

            return await _dbCollection.FindAsync(filter).Result.FirstOrDefaultAsync();
        }

        public GenericPropertiesDto GetExistentGenericProperties()
        {
            return new GenericPropertiesDto()
            {
                ListOfProperties = (from e in _dbCollection.AsQueryable<DescriptionModel>()
                                    where e.hub.Satellite != null
                                    select new { e.hub.Satellite.Attributes })
                         .ToList()
                         .SelectMany(a => a.Attributes, (a, attributes) => attributes.Name)
                         .Distinct()
                         .ToList()
            };
        }
    }
}
