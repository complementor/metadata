using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbAccessLayer.DataService.Dtos;
using System;
using System.Collections.Generic;

namespace MongoDbAccessLayer.DomainModels
{
    public class CollaborationModel
    {
        [BsonId]
        public ObjectId _id { get; set; }
        public ObjectId DocumentId { get; set; }
        public DateTime DateTime { get; set; }
        public List<Comment> Sat_Collaborative_Description { get; set; }
        public List<Tag> Sat_Collaborative_Tag { get; set; }
        public string Sat_Collaborative_Type { get; set; }
    }
}
