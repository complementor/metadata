using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDbAccessLayer.DomainModels
{
    public class CollaborationModel
    {
        [BsonId]
        public ObjectId _id { get; set; }
        public string DocumentId { get; set; }
        public string AgentId { get; set; }
    }
}
