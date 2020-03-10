using MongoDbAccessLayer.DataService.Dtos;
using System.Collections.Generic;

namespace MongoDbAccessLayer.DataService.Contracts
{
    public interface IDocumentRepository
    {
        VideoMetadataDto Get(string id);
        List<VideoInfoDto> GetAll();
    }
}