using MongoDbAccessLayer.DomainModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MongoDbAccessLayer.Context.Repository
{
    public interface ISearchRepository
    {
        public Task<List<IndexModel>> SearchOntoIndexesAsync(string searchQuery);
        public Task<List<DescriptionModel>> SearchOntoDescriptionAsync(string searchQuery);
    }
}