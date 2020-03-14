using Microsoft.Extensions.Options; 
using MongoDB.Driver;
using MongoDbAccessLayer.Context;
using MongoDbAccessLayer.Context.Repositories; 
using Moq;
using NUnit.Framework;
using System; 
using System.Linq;

namespace MongoDbAccessLayerTest.RepositoriesTest
{
    public static class DocumentRepositoryTests
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
                var exception = Assert.Throws<ArgumentNullException>(() => new DocumentRepository(null));

                Assert.AreEqual("context", exception.ParamName);
            }

            [Test]
            public void WhenValidContext_DoesntThrowException()
            {
                Assert.DoesNotThrow(() => new DocumentRepository(context));
            }
        }


        public class GetAll
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
            public void GetAllTest()
            {
                //arrage
                var documentRepository = new DocumentRepository(context);

                //act
                var result = documentRepository.GetAll();

                // "_id" : "5e6521e67576503af48640a1", 
                //"Source" : "E:/POZE/2019/ .MP4", 
                //"Name" : " ",

                var selectOne = result.FirstOrDefault(x => x.VideoId == "5e6521e67576503af48640a1");
                
                //assert
                Assert.AreEqual(3434, result.Count());
                Assert.AreEqual(" ", selectOne.Title);
                //Assert.AreEqual("E:/POZE/2019/ .MP4", selectOne["Source"]);

            }

            [Test]
            public void GetTest()
            {
                //arrage
                var documentRepository = new DocumentRepository(context);

                //act
                var result = documentRepository.Get("5e6521e67576503af48640a1");

                //assert
                Assert.NotNull(result);
                Assert.AreEqual(" ", result.Title);
            }


            [Test]
            public void GetExistentGenericPropertiesTest()
            {
                //arrage
                var documentRepository = new DocumentRepository(context);

                //act
                var result = documentRepository.GetExistentGenericProperties();

                //assert
                Assert.NotNull(result);
            }

            [Test]
            public void SearchByPropertyTest()
            {
                //arrage
                var documentRepository = new DocumentRepository(context);

                //act
                var result = documentRepository.SearchByProperty("frameSizeWidth", "320");

                //assert
                Assert.NotNull(result);
            }


            [Test]
            public void SearchTest()
            {
                //arrage
                var documentRepository = new DocumentRepository(context);

                //act
                var result = documentRepository.Search("IsoMedia File Produced by Google");

                //assert
                Assert.NotNull(result);
            }
        }
    }
}
