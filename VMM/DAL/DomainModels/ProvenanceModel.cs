using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDbAccessLayer.DomainModels
{
    public class ProvenanceModel
    {
        [BsonId]
        public ObjectId _id { get; set; }

        [BsonElement("docId")]
        public string DocId { get; set; }
        [BsonElement("activity")]
        public string ActivityName { get; set; }
        [BsonElement("type")]
        public string Type { get; set; }
    }

  
}
