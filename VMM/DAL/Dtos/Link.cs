using System;
using System.Text.Json.Serialization;

namespace MongoDbAccessLayer.Dtos
{
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