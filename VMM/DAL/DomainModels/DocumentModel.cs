using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace MongoDbAccessLayer.DomainModels
{
    public class DocumentModel
    {
        [BsonId]
        public ObjectId _id { get; set; }
        public string IndexId { get; set; }
        public string DescriptionId { get; set; }
        public BsonDateTime DateTime { get; set; }
        public List<IndexModel> Indexes { get; set; }
        public List<DescriptionModel> Descriptions { get; set; }
    }

    public class DocModel
    {
        [BsonId]
        public ObjectId _id { get; set; }

        [BsonElement("WasDerivedFrom")]
        public string WasDerivedFromId { get; set; }
    }

    public class DocModelWithDescriptionModel
    {
        [BsonId]
        public ObjectId _id { get; set; }

        [BsonElement("WasDerivedFrom")]
        public string WasDerivedFrom { get; set; }

        public List<DescriptionModel> DescriptionModel { get; set; }
    }

    public class DocReadyForNode
    {
        [BsonId]
        public ObjectId DocumentId { get; set; }

        [BsonElement("WasDerivedFrom")]
        public string WasDerivedFromId { get; set; }

        public string Title{ get; set; }
        public int Position { get; set; }
    }
}
