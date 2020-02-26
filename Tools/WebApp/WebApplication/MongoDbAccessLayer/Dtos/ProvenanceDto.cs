
using System.Collections.Generic;

namespace MongoDbAccessLayer.Dtos
{
    public class ProvenanceDto
    {
        //public string Agent { get; set; }
        public List<Node> Nodes { get; set; }
        public List<Link> Links { get; set; }
    }
}
