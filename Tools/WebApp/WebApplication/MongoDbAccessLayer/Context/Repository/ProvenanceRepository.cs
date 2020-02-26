using MongoDB.Bson;
using MongoDB.Driver;
using MongoDbAccessLayer.DomainModels;
using MongoDbAccessLayer.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MongoDbAccessLayer.Context.Repository
{
   
    public class ProvenanceRepository : IProvenanceRepository
    {
        public readonly IMongoVideoDbContext _mongoContext;
        private static IMongoCollection<ProvenanceModel> _provenanceCollection;
        private static IMongoCollection<DocModel> _documentCollection;

        public ProvenanceRepository(IMongoVideoDbContext context)
        {
            _mongoContext = context ?? throw new ArgumentNullException(nameof(context));
            _provenanceCollection = _mongoContext.GetCollection<ProvenanceModel>("provenance");
            _documentCollection = _mongoContext.GetCollection<DocModel>("document");
        }

        public ProvenanceDto Get(string documentId)
        {
            var links = GetLinks(documentId);
            var nodes = GenerateNodes(links);
            return ToDto(nodes);
        }

        private static ProvenanceDto ToDto(Tuple<List<NodeTemp>, List<LinkTemp>> nodes)
        {
            var result = new ProvenanceDto();

            result.Nodes = nodes.Item1.OrderBy(x => x.Position).Select(x => new Node()
            {
                Name = x.Id,
                Type = x.Type,
            }).ToList();

            result.Links = nodes.Item2.Select(x => new Link()
            {
                Source = x.Source.Position,
                Target = x.Target.Position,
                Type = x.RelationshipType,
                Datetime = DateTime.Now,
            }).ToList();

            return result;
        }

        private static Tuple<List<NodeTemp>, List<LinkTemp>> GenerateNodes(List<LinkTemp> links)
        {
            var nodes = new List<NodeTemp>();
            int position = 0;
            foreach (var item in links)
            {
                var node = nodes.FirstOrDefault(x => string.Equals(x.Id, item.Source.Id, StringComparison.InvariantCultureIgnoreCase));
                if (node == null)
                {
                    var newNode = new NodeTemp()
                    {
                        Id = item.Source.Id,
                        Type = item.Source.Type,
                        Position = position,
                    };
                    item.Source.Position = position;
                    nodes.Add(newNode);
                    position = (newNode.Position) + 1;
                }
                else
                {
                    item.Source.Position = node.Position;
                }

                var targetNode = nodes.FirstOrDefault(x => string.Equals(x.Id, item.Target.Id, StringComparison.InvariantCultureIgnoreCase));
                if (targetNode == null)
                {
                    var newNode = new NodeTemp()
                    {
                        Id = item.Target.Id,
                        Type = item.Target.Type,
                        Position = position,
                    };

                    item.Target.Position = position;
                    nodes.Add(newNode);
                    position = (newNode.Position) + 1;
                }
                else
                {
                    item.Target.Position = targetNode.Position;
                }
            }

            return new Tuple<List<NodeTemp>, List<LinkTemp>>(nodes, links);
        }

        private static List<LinkTemp> GetLinks(string documentId)
        {
            List<DocModel> getAllPreviousVersions = GetAllPreviousVersions(documentId);
            var links = new List<LinkTemp>();
            foreach (var r in getAllPreviousVersions)
            {
                if (r.WasDerivedFromId != null && !string.IsNullOrWhiteSpace(r.WasDerivedFromId))
                {
                    var newLinK = new LinkTemp
                    {
                        Source = new NodeTemp() { Id = r._id.ToString(), Type = "entity" },
                        RelationshipType = "wasDerivedFrom",
                        Target = new NodeTemp() { Id = r.WasDerivedFromId, Type = "entity" },
                    };
                    links.Add(newLinK);
                }
            }

            foreach (var item in getAllPreviousVersions.Where(x => x._id != null).Select(x => x._id).ToList())
            {
                var prov = GetEntityActivities(item.ToString());

                foreach (var r in prov)
                {
                    if (r.Type == "wasGeneretedBy")
                    {
                        var newLinK = new LinkTemp
                        {
                            Source = new NodeTemp() { Id = r.DocId, Type = "entity" },
                            RelationshipType = r.Type,
                            Target = new NodeTemp() { Id = r.ActivityName, Type = "activity" }
                        };
                        links.Add(newLinK);
                    }
                    if (r.Type == "used")
                    {
                        var newLinK = new LinkTemp
                        {
                            Source = new NodeTemp() { Id = r.ActivityName, Type = "activity" },
                            RelationshipType = r.Type,
                            Target = new NodeTemp() { Id = r.DocId, Type = "entity" },
                        };
                        links.Add(newLinK);
                    }
                }
            }

            return links;
        }
        private static List<DocModel> GetAllPreviousVersions(string v)
        {

            var collection = new List<DocModel>();

            while (v != null && !string.IsNullOrEmpty(v))
            {
                var filter = Builders<DocModel>.Filter.Eq("_id", new ObjectId(v));
                var result = _documentCollection.Find(filter).FirstOrDefault();
                if (result != null)
                {
                    collection.Add(result);

                    if (result.WasDerivedFromId != null && !string.IsNullOrEmpty(result.WasDerivedFromId))
                    {
                        v = result.WasDerivedFromId;
                    }
                    else
                    {
                        v = null;
                    }
                }
                else
                {
                    v = null;
                }
            }

            return collection.ToList();
        }

        private static List<ProvenanceModel> GetEntityActivities(string v)
        {
            var filter = Builders<ProvenanceModel>.Filter.Eq("docId", v);
            return _provenanceCollection.Find(filter).ToList();
        }
    }







    public class LinkTemp
    {
        public NodeTemp Source { get; set; }
        public NodeTemp Target { get; set; }
        public string RelationshipType { get; set; }
        public string Value { get; set; }

    }
    public class NodeTemp
    {
        public string Type { get; set; }
        public string Id { get; set; }
        public int Position { get; set; }
        public double Value { get; set; }
    }

}




