using System.Text.Json.Serialization;

namespace MongoDbAccessLayer.Dtos
{
    public class Node
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }
        [JsonPropertyName("name")]
        public string Id { get; set; }
        [JsonPropertyName("value")]
        public double Value { get; set; }
    }
}