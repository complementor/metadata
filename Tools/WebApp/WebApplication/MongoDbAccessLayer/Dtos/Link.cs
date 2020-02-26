using System;

namespace MongoDbAccessLayer.Dtos
{
    public class Link
    {
        public int Source { get; set; }
        public int Target { get; set; }
        public string Type { get; set; }
        public DateTime Datetime{ get; set; }
    }
}