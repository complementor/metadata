using Newtonsoft.Json;

namespace MediaOntologyMapping.Models
{
    public class OMRAttribute
    {
        [JsonProperty("Name")]
        public  string Name { get; set; }
        
        [JsonProperty("Value")]
        public string Value { get; set; }

        [JsonProperty("Standard")]
        public string Standard { get; set; }
    }
}