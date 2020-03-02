using MongoDbAccessLayer.DomainModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MongoDbAccessLayer.DataService.Contracts
{
    public interface ISearchRepository
    {
        public Task<List<IndexModel>> SearchOntoIndexesAsync(string searchQuery);
        public Task<List<DescriptionModel>> SearchOntoDescriptionAsync(string searchQuery);
    }
}