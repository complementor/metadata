using System.Collections.Generic;
using System.Threading.Tasks;

namespace MongoDbAccessLayer.DataService.Contracts
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<TEntity> Get(string id);
        Task<IEnumerable<TEntity>> GetAll();
    }
}