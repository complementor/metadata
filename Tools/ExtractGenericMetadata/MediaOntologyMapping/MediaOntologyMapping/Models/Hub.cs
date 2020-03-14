using MongoDB.Bson;

namespace MediaOntologyMapping.Models
{
    public class Hub
    {
        public BsonDateTime Date { get; set; }
        public object Satellites { get; set; }
    }
}
