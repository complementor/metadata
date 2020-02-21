using System;

namespace MongoDbAccessLayer.Dtos
{
    public class VideoInfoDto
    {
        public Guid VideoId { get; set; }
        public string Title { get; set; }
        public string Standard { get; set; }
        public string Duration { get; set; }
        public double Score { get; set; }
    }
}
