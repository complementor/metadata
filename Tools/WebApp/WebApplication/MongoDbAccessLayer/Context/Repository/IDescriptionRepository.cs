using MongoDbAccessLayer.DomainModels;
using MongoDbAccessLayer.Dtos;

namespace MongoDbAccessLayer.Context.Repository
{
    public interface IDescriptionRepository : IBaseRepository<DescriptionModel> 
    {
        GenericPropertiesDto GetExistentGenericProperties();
    }
}