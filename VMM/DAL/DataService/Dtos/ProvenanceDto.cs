
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MongoDbAccessLayer.DataService.Dtos
{
    public class ProvenanceDto
    {
        //public string Agent { get; set; }
        public List<Node> Nodes { get; set; }
        public List<Link> Links { get; set; }
    }

    public class Node
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("value")]
        public double Value { get; set; }
        //[JsonPropertyName("Datetime")]
        //public DateTime Datetime { get; set; }
    }
    public class Link
    {
        [JsonPropertyName("source")]
        public int Source { get; set; }
        [JsonPropertyName("target")]
        public int Target { get; set; }
        [JsonPropertyName("type")]
        public string Type { get; set; }
        [JsonPropertyName("value")]
        public double Value { get; set; }

        public DateTime Datetime { get; set; }
    }
}