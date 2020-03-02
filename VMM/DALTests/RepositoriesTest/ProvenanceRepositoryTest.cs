using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDbAccessLayer.Context;
using MongoDbAccessLayer.Context.Repository;
using MongoDbAccessLayer.DomainModels;
using MongoDbAccessLayer.Dtos;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MongoDbAccessLayerTest.RepositoriesTest
{
    public static class ProvenanceRepositoryTest
    {
        public class Constructor
        {
            MongoVideoDbContext context;
            Mock<IOptions<MongoSettings>> mockConnection;
            [SetUp]
            public void SetUp()
            {
                MongoSettings mongoSettings = new MongoSettings()
                {
                    Connection = "mongodb://127.0.0.1:27017/?compressors=zlib&readPreference=primary&gssapiServiceName=mongodb&appname=MongoDB%20Compass%20Community&ssl=false",
                    DatabaseName = "metadata"
                };
                mockConnection = new Mock<IOptions<MongoSettings>>();
                mockConnection.Setup(ms => ms.Value).Returns(mongoSettings);

                context = new MongoVideoDbContext(mockConnection.Object);
            }

            [Test]
            public void WhenNull_ShouldThrowException()
            {
                var exception = Assert.Throws<ArgumentNullException>(() => new ProvenanceRepository(null));

                Assert.AreEqual("context", exception.ParamName);
            }

            [Test]
            public void WhenValidContext_DoesntThrowException()
            {
                Assert.DoesNotThrow(() => new ProvenanceRepository(context));
            }
        }


        public class Get
        {
            MongoVideoDbContext context;
            Mock<IOptions<MongoSettings>> mockConnection;
            [SetUp]
            public void SetUp()
            {
                MongoSettings mongoSettings = new MongoSettings()
                {
                    Connection = "mongodb://127.0.0.1:27017/?compressors=zlib&readPreference=primary&gssapiServiceName=mongodb&appname=MongoDB%20Compass%20Community&ssl=false",
                    DatabaseName = "metadata"
                };
                mockConnection = new Mock<IOptions<MongoSettings>>();
                mockConnection.Setup(ms => ms.Value).Returns(mongoSettings);

                context = new MongoVideoDbContext(mockConnection.Object);
            }

            [Test]
            public void WhenIdIsEmpty_ThrowsException()
            {
                //arrage
                var provenanceRepository = new ProvenanceRepository(context);
                ArrangeProvenanceModel();

                //act
                var result = provenanceRepository.Get("5e5648f60042cf1df5214403");

                //assert
                var nodes = new List<Node>() {
                 new Node() { Name = "NewTitle", Type = "entity", Value = 0.2 },
                 new Node() { Name = "NewTitle", Type = "entity", Value = 0.2 },
                 new Node() { Name = "NewTitle", Type = "entity", Value = 0.2 },
                 //new Node() { Name = "5e5648f60042cf1df5214403", Type = "entity", Value = 0.2 },
                 //new Node() { Name = "5e5648f60042cf1df5214402", Type = "entity", Value = 0.2 },
                 //new Node() { Name = "5e5648f60042cf1df5214401", Type = "entity", Value = 0.2 },
                 new Node() { Name = "MetadataExtraction", Type = "activity", Value = 0.2 },
                };
                var links = new List<Link>()
                {
                    new Link() {Source = 0, Type = "wasDerivedFrom", Target= 1, Value = 0.2},
                    new Link() {Source = 1, Type = "wasDerivedFrom", Target= 2, Value = 0.2},
                    new Link() {Source = 1, Type = "wasGeneratedBy", Target= 3, Value = 0.2},
                    new Link() {Source = 3, Type = "used", Target= 2, Value = 0.2},
                };
                var doc = new ProvenanceDto() { Links = links, Nodes = nodes };
                Assert.AreEqual(doc.Links.Count, result.Links.Count);
                Assert.AreEqual(doc.Links.First().Source, result.Links.First().Source);
                Assert.AreEqual(doc.Links.First().Target, result.Links.First().Target);
                Assert.AreEqual(doc.Links.First().Type, result.Links.First().Type);
                Assert.AreEqual(doc.Links.Last().Source, result.Links.Last().Source);
                Assert.AreEqual(doc.Links.Last().Target, result.Links.Last().Target);

                Assert.AreEqual(doc.Nodes.Count, result.Nodes.Count);
                Assert.AreEqual(doc.Nodes.First().Name, result.Nodes.First().Name);
                Assert.AreEqual(doc.Nodes.Last().Type, result.Nodes.Last().Type);


                DiscardProvenanceModel();
            }

            private void ArrangeProvenanceModel()
            {
                var prov = context.GetCollection<BsonDocument>("provenance");
                var doc = context.GetCollection<BsonDocument>("document");
                var descr = context.GetCollection<BsonDocument>("description");

                DiscardProvenanceModel();

                var newDocuments = new List<BsonDocument> {
                    new BsonDocument { { "_id", ObjectId.Parse("5e5648f60042cf1df5214401") } },
                    new BsonDocument { { "_id", ObjectId.Parse("5e5648f60042cf1df5214402") }, { "WasDerivedFrom", "5e5648f60042cf1df5214401"} },
                    new BsonDocument { { "_id", ObjectId.Parse("5e5648f60042cf1df5214403") }, { "WasDerivedFrom", "5e5648f60042cf1df5214402" } }
                };

                var newProvenance = new List<BsonDocument>
                {
                    new BsonDocument { { "docId", "5e5648f60042cf1df5214402" }, { "type", "wasGeneretedBy" }, { "activity", "MetadataExtraction" }},
                    new BsonDocument { { "docId", "5e5648f60042cf1df5214401" }, { "type", "used" }, { "activity", "MetadataExtraction" }},
                };

                var newDescription = new BsonDocument { { "DocumentId", ObjectId.Parse("5e5648f60042cf1df5214403") },
                    { "DateTime", BsonDateTime.Create(DateTime.Now) },
                    { "Title", "DocumentNo1" },
                    { "Hub", new BsonDocument { { "Date", "cow" },
                        {"Satellite", new BsonDocument {
                            { "Attributes", new BsonArray(){ new BsonDocument { { "Name", "Title" }, { "Value", "NewTitle" }, { "Standard", "DC" }}}}
                        }}
                    } } 
                };


                doc.InsertMany(newDocuments);
                prov.InsertMany(newProvenance);
                descr.InsertOne(newDescription);
            }

            private void DiscardProvenanceModel()
            {
                var prov = context.GetCollection<BsonDocument>("provenance");
                var doc = context.GetCollection<BsonDocument>("document");
                var descr = context.GetCollection<BsonDocument>("description");

                doc.DeleteMany(Builders<BsonDocument>.Filter.Eq( "_id", ObjectId.Parse("5e5648f60042cf1df5214401")));
                doc.DeleteMany(Builders<BsonDocument>.Filter.Eq( "_id", ObjectId.Parse("5e5648f60042cf1df5214402")));
                doc.DeleteMany(Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse("5e5648f60042cf1df5214403")));
                prov.DeleteMany(Builders<BsonDocument>.Filter.Eq("docId", "5e5648f60042cf1df5214402"));
                prov.DeleteMany(Builders<BsonDocument>.Filter.Eq("docId", "5e5648f60042cf1df5214401"));

                descr.DeleteMany(Builders<BsonDocument>.Filter.Eq("DocumentId", ObjectId.Parse("5e5648f60042cf1df5214403")));
            }
        }
    }
}
