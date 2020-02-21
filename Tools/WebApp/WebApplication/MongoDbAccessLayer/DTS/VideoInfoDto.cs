using System;

namespace MongoDbAccessLayer.DTS
{
    public class VideoInfoDto
    {
        public Guid VideoId { get; set; }
        public string Title { get; set; }
        public string Stadnard { get; set; }
        public string Duration { get; set; }
        public double Score { get; set; }
    }
}
