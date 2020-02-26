﻿using MongoDB.Bson;
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

    internal class DocModel
    {
        [BsonId]
        public ObjectId _id { get; set; }
        [BsonElement("IndexItem")]
        public string IndexItem { get; set; }
        [BsonElement("Description")]
        public string DescriptionItem { get; set; }

        [BsonElement("WasDerivedFrom")]
        public string WasDerivedFromId { get; set; }

    }
}
