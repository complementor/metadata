using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDbAccessLayer.Context;
using MongoDbAccessLayer.Context.Repository;
using MongoDbAccessLayer.DomainModels;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Attribute = MongoDbAccessLayer.DomainModels.Attribute;

namespace MongoDbAccessLayerTest.RepositoriesTest
{
    public static class DescriptionRepositoryTest
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

        public class SearchByProperty
        {
            MongoVideoDbContext context;
            private MongoSettings _settings;
            private Mock<IOptions<MongoSettings>> _mockOptions;
            private Mock<IMongoDatabase> _mockDB;
            private Mock<IMongoClient> _mockClient;

            [SetUp]
            public void SetUp()
            {
                _settings = new MongoSettings()
                {
                    Connection = "mongodb://127.0.0.1:27017/?compressors=zlib&readPreference=primary&gssapiServiceName=mongodb&appname=MongoDB%20Compass%20Community&ssl=false",
                    DatabaseName = "metadata"
                };

                _mockOptions = new Mock<IOptions<MongoSettings>>();
                _mockDB = new Mock<IMongoDatabase>();
                _mockClient = new Mock<IMongoClient>();

                _mockOptions.Setup(s => s.Value).Returns(_settings);
                _mockClient.Setup(c => c
                .GetDatabase(_mockOptions.Object.Value.DatabaseName, null))
                    .Returns(_mockDB.Object);
                context = new MongoVideoDbContext(_mockOptions.Object);
            }

            [Test]
            public void GetExistentGenericProperties_ReturnsListOfStrings()
            {
                //Arrange
                var listOfStringsWithDocId = new List<ListOfStringWIthDocId>() {
                    new ListOfStringWIthDocId{ PropertyName = new Attribute() { Name = "title", Value = "documentID1" }, DocumentId = ObjectId.GenerateNewId() },
                    new ListOfStringWIthDocId{ PropertyName = new Attribute() { Name = "duration", Value = "200" }, DocumentId = ObjectId.GenerateNewId() },
                    new ListOfStringWIthDocId{ PropertyName = new Attribute() { Name = "container", Value = "mp4" }, DocumentId = ObjectId.GenerateNewId() },
                };

                var descriptionRepository = new DescriptionRepository(context);
                ArrangeProvenanceModel(listOfStringsWithDocId, context);

                //Act 
                var result = descriptionRepository.SearchByProperty("title", "documentID1");
                var searchTitleWithWrongValue = descriptionRepository.SearchByProperty("title", "200");
                DiscardProvenanceModel(listOfStringsWithDocId, context);

                //Assert
                Assert.AreEqual("documentID1", result.First().Title);
                Assert.IsEmpty(searchTitleWithWrongValue);
            }
        }

        public class GetExistentGenericProperties
        {
            MongoVideoDbContext context;
            private MongoSettings _settings;
            private Mock<IOptions<MongoSettings>> _mockOptions;
            private Mock<IMongoDatabase> _mockDB;
            private Mock<IMongoClient> _mockClient;

            [SetUp]
            public void SetUp()
            {
                _settings = new MongoSettings()
                {
                    Connection = "mongodb://127.0.0.1:27017/?compressors=zlib&readPreference=primary&gssapiServiceName=mongodb&appname=MongoDB%20Compass%20Community&ssl=false",
                    DatabaseName = "metadata"
                };

                _mockOptions = new Mock<IOptions<MongoSettings>>();
                _mockDB = new Mock<IMongoDatabase>();
                _mockClient = new Mock<IMongoClient>();

                _mockOptions.Setup(s => s.Value).Returns(_settings);
                _mockClient.Setup(c => c
                .GetDatabase(_mockOptions.Object.Value.DatabaseName, null))
                    .Returns(_mockDB.Object);
                context = new MongoVideoDbContext(_mockOptions.Object);
            }

            [Test]
            public void GetExistentGenericProperties_ReturnsListOfStrings()
            {
                //Arrange
                var documentID1 = ObjectId.GenerateNewId();
                var documentID2 = ObjectId.GenerateNewId();

                var listOfStringsWithDocId = new List<ListOfStringWIthDocId>() {
                    new ListOfStringWIthDocId{ PropertyName = new Attribute() { Name =  "title", Value = "title"}, DocumentId = documentID1 },
                    new ListOfStringWIthDocId{ PropertyName = new Attribute() { Name =  "duration", Value = "andrei" }, DocumentId = documentID2 }
                };

                var descriptionRepository = new DescriptionRepository(context);
                ArrangeProvenanceModel(listOfStringsWithDocId, context);

                //Act 
                var result = descriptionRepository.GetExistentGenericProperties();
                DiscardProvenanceModel(listOfStringsWithDocId, context);
                //Assert
                Assert.True(result.ListOfProperties.Contains("title"));
                Assert.True(result.ListOfProperties.Contains("duration"));

            }
        }


        internal class ListOfStringWIthDocId
        {
            public Attribute PropertyName { get; set; }
            public ObjectId DocumentId { get; set; }
        }

        private static void ArrangeProvenanceModel(List<ListOfStringWIthDocId> listOfStringsWithDocId, MongoVideoDbContext context)
        {
            //documentID1 = "5e5648f60042cf1df5214503";
            //documentID2 = "5e5648f60042cf1df5214504";
            var descr = context.GetCollection<BsonDocument>("description");

            DiscardProvenanceModel(listOfStringsWithDocId, context);

            foreach (var item in listOfStringsWithDocId)
            {
                var newDescription = new BsonDocument { { "DocumentId", item.DocumentId },
                    { "DateTime", BsonDateTime.Create(DateTime.Now) },
                    { "Hub", new BsonDocument { { "Date", "cow" },
                        {"Satellite", new BsonDocument {
                            { "Attributes", new BsonArray(){ new BsonDocument { { "Name", item.PropertyName?.Name }, { "Value", item.PropertyName?.Value }, { "Standard", "DC" }}}},
                        }}
                    } }
                    };
                descr.InsertOne(newDescription);
            }
        }

        private static void DiscardProvenanceModel(List<ListOfStringWIthDocId> listOfStringsWithDocId, MongoVideoDbContext context)
        {
            var descr = context.GetCollection<BsonDocument>("description");
            foreach (var item in listOfStringsWithDocId)
            {
                descr.DeleteOne(Builders<BsonDocument>.Filter.Eq("DocumentId", item.DocumentId));
            }
        }
    }
}