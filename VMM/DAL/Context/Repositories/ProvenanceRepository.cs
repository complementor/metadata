using MongoDB.Bson;
using MongoDB.Driver;
using MongoDbAccessLayer.DomainModels;
using MongoDbAccessLayer.DataService.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using MongoDbAccessLayer.DataService.Contracts;

namespace MongoDbAccessLayer.Context.Repositories
{
    public class ProvenanceRepository : IProvenanceRepository
    {
        private static IMongoVideoDbContext _mongoContext;
        private static IMongoCollection<ProvenanceModel> _provenanceCollection;
        private static IMongoCollection<DocModel> _documentCollection;
        private static IMongoCollection<DescriptionModel> _descriptionCollection;

        public ProvenanceRepository(IMongoVideoDbContext context)
        {
            _mongoContext = context ?? throw new ArgumentNullException(nameof(context));
            _provenanceCollection = _mongoContext.GetCollection<ProvenanceModel>("provenance");
            _documentCollection = _mongoContext.GetCollection<DocModel>("document");
            _descriptionCollection = _mongoContext.GetCollection<DescriptionModel>("description");
        }

        public ProvenanceDto Get(string documentId)
        {
            var links = GetLinks(documentId);
            var nodesAndLink = GenerateNodes(links);

            return TransformToProvenanceDto(nodesAndLink);
        }

        private static ProvenanceDto TransformToProvenanceDto(Tuple<List<NodeTemp>, List<LinkTemp>> nodes)
        {
            var result = new ProvenanceDto();

            result.Nodes = nodes.Item1?.OrderBy(x => x.Position).Select(x => new Node()
            {
                Name = x.Type == "entity" ? x.Name : x.Id,
                Type = x.Type,
            }).ToList();

            result.Links = nodes.Item2?.Select(x => new Link()
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
                        Name = item.Source.Name,
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
                        Name = item.Target.Name,
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
            List<DocReadyForNode> getAllPreviousVersions = GetAllPreviousVersions(documentId);
            var links = new List<LinkTemp>();
            foreach (var document in getAllPreviousVersions)
            {
                if (document.WasDerivedFromId != null && !string.IsNullOrWhiteSpace(document.WasDerivedFromId))
                {
                    var newLinK = new LinkTemp
                    {
                        Source = new NodeTemp() { Id = document.DocumentId.ToString(), Type = "entity", Name = document.Title },
                        RelationshipType = "wasDerivedFrom",
                        Target = new NodeTemp() { Id = document.WasDerivedFromId, Type = "entity", Name = document.Title },
                    };
                    links.Add(newLinK);
                }
            }

            foreach (var item in getAllPreviousVersions.Where(x => x.DocumentId != default).Select(x => new { x.DocumentId, x.Title }).ToList())
            {
                var prov = GetEntityActivities(item.DocumentId.ToString());

                foreach (var activity in prov)
                {
                    if (activity.Type == "wasGeneretedBy")
                    {
                        var newLinK = new LinkTemp
                        {
                            Source = new NodeTemp() { Id = activity.DocId, Type = "entity", Name = item.Title },
                            RelationshipType = activity.Type,
                            Target = new NodeTemp() { Id = activity.ActivityName, Type = "activity" }
                        };
                        links.Add(newLinK);
                    }
                    if (activity.Type == "used")
                    {
                        var newLinK = new LinkTemp
                        {
                            Source = new NodeTemp() { Id = activity.ActivityName, Type = "activity" },
                            RelationshipType = activity.Type,
                            Target = new NodeTemp() { Id = activity.DocId, Type = "entity", Name = item.Title},
                        };
                        links.Add(newLinK);
                    }
                }
            }

            return links;
        }
        private static List<DocReadyForNode> GetAllPreviousVersions(string currentDocumentId)
        {
            var listOfPreNodes = new List<DocReadyForNode>();

            while (currentDocumentId != null && !string.IsNullOrEmpty(currentDocumentId))
            {
                var document = _documentCollection.Aggregate()
               .Match(Builders<DocModel>.Filter.Eq("_id", new ObjectId(currentDocumentId)))
               .Lookup(
                   foreignCollection: _descriptionCollection,
                   localField: a => a._id,
                   foreignField: b => b.DocumentId,
                   @as: (DocModelWithDescriptionModel eo) => eo.DescriptionModel)
               .FirstOrDefault();

               // .Project(p => new { DocumentId = p._id, WasDerivedFromId = p.WasDerivedFrom, Descriptions = p.DescriptionModel[0] })
               //?
                //create preNode and retrieve title
                if (document == null)
                {
                    currentDocumentId = null;
                    continue;
                }
                else
                {
                    currentDocumentId = document.WasDerivedFrom;
                }

                listOfPreNodes.Add(new DocReadyForNode()
                {
                    DocumentId = document._id,
                    Title = FindValue(document?.DescriptionModel, "title"),
                    WasDerivedFromId = currentDocumentId
                });
            }

            return listOfPreNodes.ToList();
        }
        private static string FindValue(List<DescriptionModel> descriptionModel, string attribute)
        {
            if (descriptionModel == null || descriptionModel.Count == 0) return "no title";
            var attributes = descriptionModel?.First()?.hub?.Satellite?.Attributes;
            if (attributes == null) return "no title";
            return attributes.Where(x => string.Equals(x.Name, attribute, StringComparison.InvariantCultureIgnoreCase)).Select(x => x.Value).FirstOrDefault();
        }
        private static List<ProvenanceModel> GetEntityActivities(string documentId)
        {
            var filter = Builders<ProvenanceModel>.Filter.Eq("docId", documentId);
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
        public string Name { get;  set; }
    }

}




