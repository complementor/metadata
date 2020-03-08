using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace MediaOntologyMapping.Models
{
    public class Link
    {
        [BsonId]
        public ObjectId _id { get; set; }
        public string Source { get; set; }
        public string Name { get; set; }
        public List<Hub> Description { get; set; }
        public List<Hub> Features { get; internal set; }
    }
}