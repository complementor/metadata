using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace MongoDbAccessLayer.Models
{
    public class Generic
    {
        [BsonId]
        public ObjectId _id { get; set; }
        [BsonElement("id")]
        public string id { get; set; }
        [BsonElement("Hub")]
        public HubGeneric hub { get; set; }
        public OriginalHubGeneric OriginalHub { get; set; }
        public double score { get; set; }
        public string Source { get; set; }
    }

    public class OriginalHubGeneric
    {
        public string Date { get; set; }
        public object Satellite { get; set; }
    }

    public class HubGeneric
    {
        public string Date { get; set; }
        public SatGeneric Satellite { get; set; }
    }

    public class SatGeneric
    {
        public List<Attribute> Attributes { get; set; }
    }

    public class Attribute
    {
        public string Name { get; set; }

        public string Value { get; set; }

        public string Standard { get; set; }
    }
}