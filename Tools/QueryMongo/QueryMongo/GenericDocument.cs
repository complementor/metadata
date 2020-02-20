using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace QueryMongo
{
    public class GenericDocument
    {

        [BsonId]
        public ObjectId _id { get; set; }
        [BsonElement("Id")]
        public string Id { get; set; }
        [BsonElement("Hub")]
        public HubGeneric hub { get; set; }
        public double score { get; set; }
       
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

    public enum OMRAttributes
    {
        title,
        description,
        language,
        frameSizeWidth,
        frameSizeHeight,
        identifier,
        creator,
        date,
        duration,
        keyword,
        relation,
        genre,
        collection,
        copyright,
        format,
        audioSampleRate,
        contributor,
        compression,
    }
    
    public class Attribute
    {
        [JsonProperty("title")]
        public CommonAttribute title{ get; set; }

        [JsonProperty("Name")]
        public CommonAttribute Name { get; set; }
    }

    public class CommonAttribute
    {
        [JsonProperty("Name")]
        public static string Name { get; set; }

        [JsonProperty("Value")]
        public string Value { get; set; }

        [JsonProperty("Standard")]
        public string Standard { get; set; }
    }
}