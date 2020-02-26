using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Core.Misc;
using MongoDbAccessLayer.Context;
using Moq;
using NUnit.Framework;
using System;

namespace MongoDbAccessLayerTest
{
    public static class MongoVideoDbContextTest
    {
        public class TestConstructor
        {
            [Test]
            public void WhenNull_ShouldThrowException()
            {
                var exception = Assert.Throws<ArgumentNullException>(() => new MongoVideoDbContext(null));

                Assert.AreEqual("configuration", exception.ParamName);
            }

            [Test]
            public void ShouldNotThrowException()
            {
                MongoSettings mongoSettings = new MongoSettings() { Connection = "mongodb://127.0.0.1:27017/?compressors=zlib&readPreference=primary&gssapiServiceName=mongodb&appname=MongoDB%20Compass%20Community&ssl=false", DatabaseName = "metadata" };
                var mock = new Mock<IOptions<MongoSettings>>();
                mock.Setup(ms => ms.Value).Returns(mongoSettings);

                Assert.DoesNotThrow(() => new MongoVideoDbContext(mock.Object));
            }
        }


        public class GetCollectionTest
        {
            MongoVideoDbContext ctx;
            private MongoSettings _settings;
            private Mock<IOptions<MongoSettings>> _mockOptions;
            private Mock<IMongoDatabase> _mockDB;
            private Mock<IMongoClient> _mockClient;

            [SetUp]
            public void Setup()
            {
                _settings = new MongoSettings()
                {
                    Connection = "mongodb://127.0.0.1:27017/?compressors=zlib&readPreference=primary&gssapiServiceName=mongodb&appname=MongoDB%20Compass%20Community&ssl=false",
                    DatabaseName = "metadata"
                    //Connection = "mongodb://tes123",
                    //DatabaseName = "TestDB"
                };

                _mockOptions = new Mock<IOptions<MongoSettings>>();
                _mockDB = new Mock<IMongoDatabase>();
                _mockClient = new Mock<IMongoClient>();

            }

            [TestCase("undefined")]
            [TestCase("")]
            [TestCase("CollectionNameThatWillNeverBeUsed")]
            public void WhenCollectionNameDoesntExist_ShouldThrowArgumentNullFoundException(string collectionName)
            {
                //Arrange
                _mockOptions.Setup(s => s.Value).Returns(_settings);
                _mockClient.Setup(c => c
                .GetDatabase(_mockOptions.Object.Value.DatabaseName, null))
                    .Returns(_mockDB.Object);

                //Act 
                var context = new MongoVideoDbContext(_mockOptions.Object);
                var collection = Assert.Throws<ArgumentNullException>( () => context.GetCollection<Feature>(collectionName));

                //Assert
                Assert.AreEqual("collectionName", collection.ParamName);

            }

            [TestCase("index")]
            [TestCase("description")]
            //[TestCase("provenance")]
            //[TestCase("collaboration") ]  
            public void WhenContextIsValud_TheCollectionIsNotNull(string collectionName)
            {
                //Arrange
                _mockOptions.Setup(s => s.Value).Returns(_settings);
                _mockClient.Setup(c => c
                .GetDatabase(_mockOptions.Object.Value.DatabaseName, null))
                    .Returns(_mockDB.Object);

                //Act 
                var context = new MongoVideoDbContext(_mockOptions.Object);
                var collection = context.GetCollection<Feature>(collectionName);

                //Assert
                Assert.IsNotNull(collection);

            }
        }
    }
}