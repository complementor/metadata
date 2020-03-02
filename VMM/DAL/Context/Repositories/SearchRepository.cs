using MongoDB.Driver;
using MongoDbAccessLayer.DataService.Contracts;
using MongoDbAccessLayer.DomainModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MongoDbAccessLayer.Context.Repositories
{
    public class SearchRepository : ISearchRepository
    {
        public readonly IMongoVideoDbContext _mongoContext;
        public IMongoCollection<DescriptionModel> _descriptionCollection;
        public IMongoCollection<IndexModel> _indexCollection;

        public SearchRepository(IMongoVideoDbContext context)
        {
            _mongoContext = context;
            _descriptionCollection = _mongoContext.GetCollection<DescriptionModel>("description");
            _indexCollection = _mongoContext.GetCollection<IndexModel>("index");
        }

        public async Task<List<IndexModel>> SearchOntoIndexesAsync(string searchQuery)
        {
            return await _indexCollection
                    .Find(Builders<IndexModel>.Filter.Text(searchQuery, new TextSearchOptions() { CaseSensitive = false, DiacriticSensitive = false, Language = "english" }))
                    .Project<IndexModel>(Builders<IndexModel>.Projection.MetaTextScore("score"))
                    .Sort(Builders<IndexModel>.Sort.MetaTextScore("score"))
                    .ToListAsync();
        }

        public async Task<List<DescriptionModel>> SearchOntoDescriptionAsync(string searchQuery)
        {
            return await _descriptionCollection
                    .Find(Builders<DescriptionModel>.Filter.Text(searchQuery, new TextSearchOptions() { CaseSensitive = false, DiacriticSensitive = false, Language = "english" }))
                    .Project<DescriptionModel>(Builders<DescriptionModel>.Projection.MetaTextScore("score"))
                    .Sort(Builders<DescriptionModel>.Sort.MetaTextScore("score"))
                    .ToListAsync();
        }

    }
}