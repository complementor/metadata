using MongoDbAccessLayer.DomainModels;
using MongoDbAccessLayer.Dtos;
using System.Collections.Generic;

namespace MongoDbAccessLayer.Context.Repository
{
    public interface IDescriptionRepository : IBaseRepository<DescriptionModel> 
    {
        GenericPropertiesDto GetExistentGenericProperties();
    }
}