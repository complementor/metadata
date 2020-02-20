using MongoDB.Bson;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace MongoDbAccessLayer.Models
{
    public class Generic
    {
        public class Link
        {
            public ObjectId Id { get; set; }
            public Hub Hub { get; set; }
            public Hub OriginalHub { get; set; }
        }

        public class Hub
        {
            public string Date { get; set; }
            public object Satellite { get; set; }
        }

        public class Satellite
        {
            public List<object> Attributes { get; set; }
        }

        public class OriginalSatellite
        {
            public JObject Attributes { get; set; }
        }

        public class Attribute
        {
            [JsonProperty("Name")]
            public static string Name { get; set; }

            [JsonProperty("Value")]
            public string Value { get; set; }

            [JsonProperty("Standard")]
            public string Standard { get; set; }
        }
    }
}
