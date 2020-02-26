using System;
using System.Text.Json.Serialization;

namespace MongoDbAccessLayer.Dtos
{
    public class Node
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("value")]
        public double Value { get; set; }
        [JsonPropertyName("value")]
        public DateTime Datetime { get; set; }
    }
}