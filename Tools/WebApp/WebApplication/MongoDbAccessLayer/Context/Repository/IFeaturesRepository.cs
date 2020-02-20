using MongoDbAccessLayer.DTS;
using MongoDbAccessLayer.Models;
using System.Collections.Generic;

namespace MongoDbAccessLayer.Context.Repository
{
    public interface IFeatureRepository : IBaseRepository<Features> 
    {
        List<VideoInfoDto> Search(string text);
    }
}