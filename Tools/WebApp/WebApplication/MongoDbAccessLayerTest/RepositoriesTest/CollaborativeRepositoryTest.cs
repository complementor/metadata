using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDbAccessLayer.Context;
using MongoDbAccessLayer.Context.Repository;
using MongoDbAccessLayer.Dtos;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Tag = MongoDbAccessLayer.Dtos.Tag;

namespace MongoDbAccessLayerTest.RepositoriesTest
{
    public static class CollaborativeRepositoryTest
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
                var exception = Assert.Throws<ArgumentNullException>(() => new CollaborativeRepository(null));

                Assert.AreEqual("context", exception.ParamName);
            }

            [Test]
            public void WhenValidContext_DoesntThrowException()
            {
                Assert.DoesNotThrow(() => new CollaborativeRepository(context));
            }
        }

        public class Get
        {
            static MongoVideoDbContext context;
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
            public void WhenIdIsEmpty_ThrowsException()
            {
                //arrage
                var documentId = ObjectId.Parse("5e57e6588df02e2350a292dd");
                CreateContext(GetCollaborationDto(), documentId);
                var collaborativeRepository = new CollaborativeRepository(context);

                ////act
                var result = collaborativeRepository.Get(documentId.ToString());
                DiscardContext(documentId);

                //assert
                Assert.AreEqual(3, result.Comments.Count);
                Assert.AreEqual(6, result.Tags.Count);
                Assert.Contains("Entertainment", result.Tags.Select(x => x.TagName).ToList());
                Assert.Contains("Peter", result.Tags.Select(x => x.UserName).ToList());
                Assert.AreEqual("Peter", result.Tags.FirstOrDefault(x => x.TagName == "Entertainment").UserName);
            }

            private static void CreateContext(CollaborationDto collaborationDto, ObjectId documentId)
            {
                var descriptions = new BsonArray();
                var tags = new BsonArray();

                foreach (var tag in collaborationDto.Tags)
                {
                    tags.Add(new BsonDocument {
                        { "TagName", tag.TagName },
                        { "UserName", tag.UserName},
                        //{ "Datetime", tag.Datetime }
                    });
                }
                foreach (var comment in collaborationDto.Comments)
                {
                    descriptions.Add(new BsonDocument {
                        { "User", comment.User },
                        { "UserIcon", comment.UserIcon },
                        { "DateTime", comment.DateTime },
                        { "Description", comment.Description },
                    });
                }

                var document = new BsonDocument { { "DocumentId", documentId },
                { "DateTime", BsonDateTime.Create(DateTime.Now)},
                new BsonDocument { { "Sat_Collaborative_Description", descriptions} } ,
                new BsonDocument{ { "Sat_Collaborative_Tag", tags} },
                new BsonDocument{ { "Sat_Collaborative_Type", "" } },
                };

                var collaborativeCollection = context.GetCollection<BsonDocument>("collaborative");
                collaborativeCollection.InsertOne(document);
            }

            private static void DiscardContext(ObjectId documentId)
            {
                var collaborativeCollection = context.GetCollection<BsonDocument>("collaborative");
                collaborativeCollection.DeleteMany(Builders<BsonDocument>
                    .Filter
                    .Eq("DocumentId", documentId));
            }

            public static CollaborationDto GetCollaborationDto()
            {
                var collaboration = new CollaborationDto
                {
                    Comments = new List<Comment>
                {
                   new Comment
                   {
                       User = "Peter",
                       UserIcon = "https://images.pexels.com/photos/220453/pexels-photo-220453.jpeg?auto=compress&cs=tinysrgb&dpr=1&w=200",
                       Description = "I did a classification task on this video.",
                       DateTime = DateTime.Now.AddDays(2).ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture)
                   },
                   new Comment
                   {
                       User = "Anne",
                       UserIcon = "https://www.joancanto.com/wp-content/uploads/2017/04/H10B0527.jpg",
                       Description = "Great job, Peter. Very useful for my work.",
                       DateTime = DateTime.Now.AddDays(3).ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture)
                   },
                   new Comment
                   {
                       User = "Thomas",
                       UserIcon = "https://cdn.vuetifyjs.com/images/lists/2.jpg",
                       Description = "Nice findings! I added a few more tags.",
                       DateTime = DateTime.Now.AddDays(4).ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture)
                   }
                },
                    Tags = new List<Tag>
                {
                    new Tag
                    {
                        TagName = "Entertainment",
                        UserName = "Peter",
                        Datetime = DateTime.Now.AddDays(2).ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture)

                    },
                    new Tag
                    {
                        TagName = "Nature",
                        UserName = "Anne",
                        Datetime = DateTime.Now.AddDays(3).ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture)
                    },
                    new Tag
                    {
                        TagName = "New Zealand",
                        UserName = "Anne",
                        Datetime = DateTime.Now.AddDays(4).ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture)
                    },
                    new Tag
                    {
                        TagName = "Travel",
                        UserName = "Anne",
                        Datetime = DateTime.Now.AddDays(5).ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture)
                    },
                    new Tag
                    {
                        TagName = "Youtube",
                        UserName = "Thomas",
                        Datetime = DateTime.Now.AddDays(2).ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture)
                    },
                    new Tag
                    {
                        TagName = "Auckland",
                        UserName = "Thomas",
                        Datetime = DateTime.Now.AddDays(6).ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture)
                    }
                }
                };

                return collaboration;
            }
        }
    }
}