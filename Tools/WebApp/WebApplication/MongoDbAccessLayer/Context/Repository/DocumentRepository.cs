using MongoDB.Bson;
using MongoDB.Driver;
using MongoDbAccessLayer.DomainModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MongoDbAccessLayer.Context.Repository
{
    public class DocumentRepository 
    {
        public readonly IMongoVideoDbContext _mongoContext;
        public IMongoCollection<DocumentModel> _documentModel;
        public IMongoCollection<DescriptionModel> _descriptionModel;

        public DocumentRepository(IMongoVideoDbContext context)
        {
            _mongoContext = context;
            _documentModel = _mongoContext.GetCollection<DocumentModel>("document");
            _descriptionModel = _mongoContext.GetCollection<DescriptionModel>("description");
        }

        public async Task<IEnumerable<DocumentModel>> GetAll()
        { 
            return await _documentModel.Aggregate()
                .Lookup<DocumentModel, DescriptionModel, DocumentModel>(
                    _descriptionModel,
                    a => a.DescriptionId,
                    b => b.id,
                    a => a.Descriptions)
                .ToListAsync();
        }

        public async Task<DocumentModel> Get(string id)
        {
            //ex. ObjectId("5e514803fa0df9b9f548f02c);
            var objectId = new ObjectId(id);

            FilterDefinition<DocumentModel> filter = Builders<DocumentModel>.Filter.Eq("_id", objectId);

            return await _documentModel.FindAsync(filter).Result.FirstOrDefaultAsync();
        }
    }
}