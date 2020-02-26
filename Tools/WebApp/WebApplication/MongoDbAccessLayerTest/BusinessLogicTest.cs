using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDbAccessLayer;
using MongoDbAccessLayer.Context;
using MongoDbAccessLayer.Dtos;
using MongoDbAccessLayer.DomainModels;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using static MongoDbAccessLayer.DomainModels.IndexModel;

namespace MongoDbAccessLayerTest
{
    public static class BusinessLogicTest
    {

        public class SearchTest
        {
            MongoVideoDbContext context;
            Mock<IOptions<MongoSettings>> mockConnection;
            [SetUp]
            public void SetUp()
            {
                MongoSettings mongoSettings = new MongoSettings() { Connection = "mongodb://127.0.0.1:27017/?compressors=zlib&readPreference=primary&gssapiServiceName=mongodb&appname=MongoDB%20Compass%20Community&ssl=false", DatabaseName = "metadata" };
                mockConnection = new Mock<IOptions<MongoSettings>>();
                mockConnection.Setup(ms => ms.Value).Returns(mongoSettings);

                context = new MongoVideoDbContext(mockConnection.Object);
            }

            [Test]
            public void TestMongoResult()
            {
                //Arrange
                var businessLogic = new BusinessLogic(mockConnection.Object);

                var guid = Guid.NewGuid();
                var insertToIndex = new IndexModel()
                {
                    Id = guid.ToString(),
                    hub = new Hub()
                    {
                        date = "redcar",
                        satellite = new List<Satellite>() {
                            new Satellite()
                                {
                                    objects = new List<CommonObjects>() {
                                        new CommonObjects() {
                                            value = new List<ObjectValue>() { new ObjectValue() { confidence = 2.22, label = "cow" } } } }
                                }}
                    }
                };
                var collectionFeature = context.GetCollection<IndexModel>("index");
                collectionFeature.InsertOne(insertToIndex);

                var insertToGeneric = new DescriptionModel()
                {
                    id = guid.ToString(),
                    hub = new HubGeneric()
                    {
                        Date = "cow",
                    }
                };
                var collectionGeneric = context.GetCollection<DescriptionModel>("description");
                collectionGeneric.InsertOne(insertToGeneric);

                //Assert
                List<VideoInfoDto> result = businessLogic.Search("cow");
                //Assert.AreEqual(3, result.Count);
                Assert.AreEqual(guid.ToString(), result.First().VideoId);

                //cleanup
                FilterDefinition<IndexModel> filter = Builders<IndexModel>.Filter.Eq("Id", guid.ToString());
                collectionFeature.DeleteMany(filter);
                FilterDefinition<DescriptionModel> filterGeneric = Builders<DescriptionModel>.Filter.Eq("Id", guid.ToString());
                collectionGeneric.DeleteMany(filterGeneric);
            }

            public class GetByIdTest
            {

                MongoVideoDbContext context;
                Mock<IOptions<MongoSettings>> mockConnection;
                [SetUp]
                public void SetUp()
                {
                    MongoSettings mongoSettings = new MongoSettings() { Connection = "mongodb://127.0.0.1:27017/?compressors=zlib&readPreference=primary&gssapiServiceName=mongodb&appname=MongoDB%20Compass%20Community&ssl=false", DatabaseName = "metadata" };
                    mockConnection = new Mock<IOptions<MongoSettings>>();
                    mockConnection.Setup(ms => ms.Value).Returns(mongoSettings);

                    context = new MongoVideoDbContext(mockConnection.Object);

                }
                [Test]
                public void TestGenericResult()
                {
                    //Arrange
                    var businessLogic = new BusinessLogic(mockConnection.Object);
                    var guid = Guid.NewGuid();
                    var insertToGeneric = new DescriptionModel()
                    {
                        id = guid.ToString(),
                        hub = new HubGeneric()
                        {
                            Date = "cow",
                            Satellite = new SatGeneric()
                            {
                                Attributes = new List<MongoDbAccessLayer.DomainModels.Attribute>()
                            {
                                new MongoDbAccessLayer.DomainModels.Attribute()
                                {
                                    Name = "Title",
                                    Value = "NewTitle",
                                    Standard = "DC",
                                },
                                new MongoDbAccessLayer.DomainModels.Attribute()
                                {
                                    Name = "Duration",
                                    Value = "40",
                                    Standard = "DC",
                                },
                                new MongoDbAccessLayer.DomainModels.Attribute()
                                {
                                    Name = "Creator",
                                    Value = "Andrei",
                                    Standard = "DC",
                                },
                                new MongoDbAccessLayer.DomainModels.Attribute()
                                {
                                    Name = "YoutubeId",
                                    Value = "123",
                                    Standard = "Youtube",
                                }
                            }
                            }
                        }
                    };
                    var collectionGeneric = context.GetCollection<DescriptionModel>("description");
                    collectionGeneric.InsertOne(insertToGeneric);

                    //Act
                    VideoMetadataDto result = businessLogic.Get(guid.ToString());

                    //Assert
                    var generic = result.Generic;

                    Assert.NotNull(result);
                    Assert.NotNull(generic);
                    Assert.AreEqual("123", result.YouTubeId);

                    Assert.AreEqual(4, result.Generic.Count);
                    Assert.AreEqual("NewTitle", result.Title);
                    Assert.AreEqual("NewTitle", result.Generic.FirstOrDefault(x => x.Name == "Title").Value);
                    Assert.AreEqual("40", result.Duration);
                    Assert.AreEqual("40", result.Generic.FirstOrDefault(x => x.Name == "Duration").Value);
                    Assert.AreEqual("123", result.YouTubeId);
                    Assert.AreEqual("123", result.Generic.FirstOrDefault(x => x.Name == "YoutubeId").Value);
                    Assert.AreEqual("Andrei", result.Generic.FirstOrDefault(x => x.Name == "Creator").Value);

                    //cleanup
                    FilterDefinition<DescriptionModel> filterGeneric = Builders<DescriptionModel>.Filter.Eq("Id", guid.ToString());
                    collectionGeneric.DeleteOne(filterGeneric);
                }


                [Test]
                public void TestFeatureResult()
                {
                    //Arrange
                    var businessLogic = new BusinessLogic(mockConnection.Object);

                    var guid = Guid.NewGuid();

                    var insertToFeature = new IndexModel()
                    {
                        Id = guid.ToString(),
                        hub = new Hub()
                        {
                            date = "redcar",
                            satellite = new List<Satellite>() {
                            new Satellite()
                            {
                                scenes = new List<MongoDbAccessLayer.DomainModels.Scene>(){
                                    new MongoDbAccessLayer.DomainModels.Scene()
                                    {
                                        scene = 1,
                                    }
                                },
                                objects = new List<CommonObjects>() {
                                    new CommonObjects() {
                                        scene = 1,
                                        value = new List<ObjectValue>() {
                                            new ObjectValue() { confidence = 2.22, label = "cow" } } } },

                            }}
                        }
                    };

                    var collectionFeature = context.GetCollection<IndexModel>("index");
                    collectionFeature.InsertOne(insertToFeature);


                    //Act
                    VideoMetadataDto result = businessLogic.Get(guid.ToString());


                    //Assert
                    Assert.NotNull(result);
                    Assert.AreEqual(1, result.Scenes.Count());
                    Assert.AreEqual("cow", result.Scenes.Where(x => x.SceneNumber == 1)
                        .Select(x => x.Objects).First().First().Name);

                    //cleanup
                    FilterDefinition<IndexModel> filter = Builders<IndexModel>.Filter.Eq("Id", guid.ToString());
                    collectionFeature.DeleteOne(filter);
                }
                [Test]
                public void TestSceneResult()
                {
                    //Arrange
                    var businessLogic = new BusinessLogic(mockConnection.Object);

                    var guid = Guid.NewGuid();

                    var insertToFeature = new IndexModel()
                    {
                        Id = guid.ToString(),
                        hub = new Hub()
                        {
                            date = "redcar",
                            satellite = new List<Satellite>() {
                            new Satellite()
                            {
                                scenes = new List<MongoDbAccessLayer.DomainModels.Scene>() {
                                    new MongoDbAccessLayer.DomainModels.Scene() {scene = 1,},
                                    new MongoDbAccessLayer.DomainModels.Scene() {scene = 2, },
                                    new MongoDbAccessLayer.DomainModels.Scene() {scene = 3,},
                            },
                                optical_character_recognition = new List<Optical_Character_Recognition>() {
                                    new Optical_Character_Recognition() {scene = 3, value = "a"},
                            },
                                speech_recognition = new List<Speech_Recognition>() {
                                    new Speech_Recognition() {scene = 3, value = "b"},
                            },
                                objects = new List<CommonObjects>() {
                                    new CommonObjects() { scene = 3, value = new List<ObjectValue>()
                                    {
                                        new ObjectValue(){
                                        confidence = 2.22,
                                        label = "car",
                                    },
                                    }
                            }

                            },
                                 sentiment_analysis = new List<Sentiment_Analysis>() {
                                    new Sentiment_Analysis() { scene = 3, value = new Sentiment_AnalysisValue(){
                                        neg = 2.22,
                                        neu = 2.34,
                                    }
                            }

                            },
                        }
                        }
                        }
                    };

                    var collectionFeature = context.GetCollection<IndexModel>("index");
                    collectionFeature.InsertOne(insertToFeature);


                    //Act
                    VideoMetadataDto result = businessLogic.Get(guid.ToString());


                    //Assert
                    Assert.NotNull(result);
                    Assert.AreEqual(3, result.Scenes.Count());
                    Assert.NotNull(result.Scenes.Where(s => s.SceneNumber == 3).ToList());
                    Assert.AreEqual(1, result.Scenes.Where(s => s.SceneNumber == 3).Select(x => x.Objects).Count());
                    Assert.AreEqual("car", result.Scenes.Where(s => s.SceneNumber == 3).Select(x => x.Objects).First().First().Name);
                    Assert.AreEqual("b", result.Scenes.Where(s => s.SceneNumber == 3).Select(x => x.Speech).FirstOrDefault());
                    Assert.AreEqual("a", result.Scenes.Where(s => s.SceneNumber == 3).Select(x => x.OCR).FirstOrDefault());
                    Assert.AreEqual(2.22, result.Scenes.Where(s => s.SceneNumber == 3).Select(x => x.Sentiment).FirstOrDefault().Negative);

                    //cleanup
                    FilterDefinition<IndexModel> filter = Builders<IndexModel>.Filter.Eq("Id", guid.ToString());
                    collectionFeature.DeleteOne(filter);
                }


                [Test]
                public void TestSpeechAggregatedResult()
                {
                    //Arrange
                    var businessLogic = new BusinessLogic(mockConnection.Object);

                    var guid = Guid.NewGuid();

                    var insertToFeature = new IndexModel()
                    {
                        Id = guid.ToString(),
                        hub = new Hub()
                        {
                            date = "redcar",
                            satellite = new List<Satellite>() {
                            new Satellite()
                            {
                                speech_recognition = new List<Speech_Recognition>() {
                                    new Speech_Recognition() {
                                       scene = 1, value = "1"   },
                                    new Speech_Recognition() {
                                       scene = 2, value = "2"   },
                                    new Speech_Recognition() {
                                       scene = 3, value = "3"   }},
                            }}
                        }
                    };

                    var collectionFeature = context.GetCollection<IndexModel>("index");
                    collectionFeature.InsertOne(insertToFeature);


                    //Act
                    VideoMetadataDto result = businessLogic.Get(guid.ToString());


                    //Assert
                    Assert.NotNull(result);
                    Assert.AreEqual("1 2 3", result.SpeechAggregated);


                    //cleanup
                    FilterDefinition<IndexModel> filter = Builders<IndexModel>.Filter.Eq("Id", guid.ToString());
                    collectionFeature.DeleteOne(filter);
                }

                [Test]
                public void TestOCRAggregatedResult()
                {
                    //Arrange
                    var businessLogic = new BusinessLogic(mockConnection.Object);

                    var guid = Guid.NewGuid();

                    var insertToFeature = new IndexModel()
                    {
                        Id = guid.ToString(),
                        hub = new Hub()
                        {
                            date = "redcar",
                            satellite = new List<Satellite>() {
                            new Satellite()
                            {
                                optical_character_recognition = new List<Optical_Character_Recognition>() {
                                    new Optical_Character_Recognition() {scene = 1, value = "1"   },
                                    new Optical_Character_Recognition() {
                                        scene = 2, value = "2"   },
                                    new Optical_Character_Recognition() {
                                        scene = 3, value = "3"   }},
                            }}
                        }
                    };

                    var collectionFeature = context.GetCollection<IndexModel>("index");
                    collectionFeature.InsertOne(insertToFeature);


                    //Act
                    VideoMetadataDto result = businessLogic.Get(guid.ToString());


                    //Assert
                    Assert.NotNull(result);
                    Assert.AreEqual("1 2 3", result.OCRAggregated);


                    //cleanup
                    FilterDefinition<IndexModel> filter = Builders<IndexModel>.Filter.Eq("Id", guid.ToString());
                    collectionFeature.DeleteOne(filter);
                }
            }
        }
    }
}