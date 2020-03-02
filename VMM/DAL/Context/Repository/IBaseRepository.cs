using System.Collections.Generic;
using System.Threading.Tasks;

namespace MongoDbAccessLayer.Context.Repository
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<TEntity> Get(string id);
        Task<IEnumerable<TEntity>> GetAll();
    }
}