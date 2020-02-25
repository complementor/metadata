using System;

namespace MongoDbAccessLayer.Dtos
{
    public class Link
    {
        public string Source { get; }
        public string Target { get; }
        public string RelationshipType { get; set; }
        public DateTime Datetime{ get;  }
    }
}