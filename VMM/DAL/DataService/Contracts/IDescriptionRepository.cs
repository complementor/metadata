using MongoDbAccessLayer.DataService.Dtos;
using MongoDbAccessLayer.DomainModels;
using System.Collections.Generic;

namespace MongoDbAccessLayer.DataService.Contracts
{
    public interface IDescriptionRepository : IBaseRepository<DescriptionModel> 
    {
        GenericPropertiesDto GetExistentGenericProperties();
        List<VideoInfoDto> SearchByProperty(string propertyName, string text);
    }
}