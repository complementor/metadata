using MongoDbAccessLayer.DTS;
using MongoDbAccessLayer.Models;
using System;
using System.Collections.Generic;

namespace MongoDbAccessLayer.Context.Repository
{
    public interface ISearchRepository : IBaseRepository<Features>
    {
        List<VideoInfoDto> Search(string text);
        VideoMetadataDto GetVideoById(Guid guid);
    }
}
