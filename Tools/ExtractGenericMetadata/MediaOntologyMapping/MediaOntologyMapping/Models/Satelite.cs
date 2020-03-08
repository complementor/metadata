using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace MediaOntologyMapping.Models
{
    public class Satellite
    {
        public string Name { get; set; }
        public List<OMRAttribute> Attributes { get; set; }
    }
    public class OriginalSatellite
    {
        public string Name { get; set; }
        public JObject Attributes { get; set; }
    }
    public class FeatureSatellite
    {
        public string Name { get; set; }
        public JObject Attributes { get; set; }
    }
}