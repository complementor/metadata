using System;
using System.Collections.Generic;

namespace MongoDbAccessLayer
{
    public interface ISearchManager
    {
        List<VideoInfoDto> Search(string text);
        VideoMetadataDto GetVideoById(Guid guid);
    }
}
