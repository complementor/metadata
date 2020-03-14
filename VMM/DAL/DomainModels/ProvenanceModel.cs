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

    public class DocModel
    {
        [BsonId]
        public ObjectId _id { get; set; }

        [BsonElement("WasDerivedFrom")]
        public string WasDerivedFromId { get; set; }
    }

    //public class DocModelWithDescriptionModel
    //{
    //    [BsonId]
    //    public ObjectId _id { get; set; }

    //    [BsonElement("WasDerivedFrom")]
    //    public string WasDerivedFrom { get; set; }

    //    public List<DescriptionModel> DescriptionModel { get; set; }
    //}

    public class DocReadyForNode
    {
        [BsonId]
        public ObjectId DocumentId { get; set; }

        [BsonElement("WasDerivedFrom")]
        public string WasDerivedFromId { get; set; }

        public string Title { get; set; }
        public int Position { get; set; }
    }
}
