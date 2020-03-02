using MongoDbAccessLayer.Dtos;
using System.Collections.Generic;

namespace MongoDbAccessLayer
{
    public interface IBusinessLogic
    {
        public List<VideoInfoDto> Search(string searchQuery);
        public VideoMetadataDto Get(string objectId);
        public List<VideoInfoDto> GetAll();
    }
}