using MongoDB.Bson;
using MongoDbAccessLayer.DTS;
using System.Collections.Generic;

namespace MongoDbAccessLayer
{
    public interface IBusinessLogic
    {
        public List<VideoInfoDto> Search(string searchQuery);
        public VideoMetadataDto Get(string objectId);
    }
}