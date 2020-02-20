using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using static QueryMongo.MongoModel;

namespace QueryMongo
{
    internal static class Program
    {
        static IMongoDatabase database;
        static void Main(string[] args)
        {


            Console.WriteLine("Connect to localhost");
            const string connectionStirng = "mongodb://127.0.0.1:27017/?compressors=zlib&readPreference=primary&gssapiServiceName=mongodb&appname=MongoDB%20Compass%20Community&ssl=false";
            MongoClient client = new MongoClient(connectionStirng);

            Console.WriteLine($"Connect to database");
            database = client.GetDatabase("metadata");

            var mongoCollection = database.GetCollection<BsonDocument>("features");
            var builder = Builders<BsonDocument>.Filter;

            TextSearchOptions textSearchOptions = new TextSearchOptions() { CaseSensitive = false, DiacriticSensitive = false, Language = "english" };
            var filter = builder.Text("cow", textSearchOptions);
            var projection = Builders<BsonDocument>.Projection.MetaTextScore("score");
            var sort = Builders<BsonDocument>.Sort.MetaTextScore("score");
            var sortedResult = mongoCollection
                .Find(filter)
                .Project<MongoModel>(projection)
                .Sort(sort)
                .ToList();



            //var mongoCollection = GetMongoDbCollection("test");

            //ExtendDocument(mongoCollection, Samples.HubWithScenes, Samples.With1AlgmDoc, "optical_character_recognition");
            //ExtendDocument(mongoCollection, Samples.With1AlgmDoc, Samples.With2AlgmDoc, "speech_recognition");
            //ExtendDocument(mongoCollection, Samples.With2AlgmDoc, Samples.With3AlgmDoc, "sentiment_analysis");
            //ExtendDocument(mongoCollection, Samples.With3AlgmDoc, Samples.With4AlgmDoc, "objects");
        }

        //private static void ExtendDocument(IMongoCollection<BsonDocument> mongoCollection, BsonDocument initialDocument,
        //    BsonDocument extendedDocument, string indexName)
        //{
        //    var builder = Builders<BsonDocument>.Filter;
        //    IMongoCollection<BsonDocument> collection = UpsertDocumentToCollection(initialDocument, mongoCollection);

        //    Func<FilterDefinition<BsonDocument>> filter = () => builder.Eq("hub.date", "14-02-2020 13:50:23");

        //    //Console.WriteLine(@"Apply the query:  SELECT COUNT(*) FROM document WHERE date=""14 - 02 - 2020 13:50:23""");
        //    //var initialHub = collection.Find(filter.Invoke())?.First()?.AsBsonDocument;
        //    //PrinJsonFormat(initialHub);

        //    collection.ReplaceOne(filter.Invoke(), extendedDocument);
        //    Console.WriteLine($" ");
        //    Console.WriteLine($"Extract and Index: {indexName}");
        //    Console.WriteLine($" ");

        //    Console.WriteLine(@"SELECT COUNT(*) FROM document WHERE scene=1");
        //    var documentAfterUpdate = collection.Find(filter.Invoke()).First().AsBsonDocument;
        //    PrintTotalNumber(documentAfterUpdate);
        //    Console.WriteLine(@"SELECT satellites FROM document WHERE scene=1");
        //    PrintTheSatellites(documentAfterUpdate);

        //    Console.ReadLine();
        //}

        //private static void PrintTheSatellites(BsonDocument initialHub)
        //{
        //    var s = BsonSerializer.Deserialize<Rootobject>(initialHub.ToJson());
        //    PrintSatellites(s.hub.satellite.ToList());
        //}

        //private static void PrintTotalNumber(BsonDocument initialHub)
        //{
        //    var s = BsonSerializer.Deserialize<Rootobject>(initialHub.ToJson());
        //    Console.WriteLine(s.hub.satellite.ToList().Count());
        //}
        //private static void PrintSatellites(List<Satellite> satellites)
        //{
        //    foreach (var satellite in satellites)
        //    {
        //        if (satellite.optical_character_recognition != null)
        //        {
        //            Console.Write($"OCR;");
        //            //Console.Write($"OCR: {satellite.optical_character_recognition?.Count}; ");
        //        }
        //        if (satellite.speech_recognition != null)
        //        {
        //            Console.Write($"Speech;");
        //        }
        //        if (satellite.sentiment_analysis != null)
        //        {
        //            Console.Write($"Sentiment;");
        //        }
        //        if (satellite.objects != null)
        //        {
        //            Console.Write($"Objects;");
        //        }
        //    }
        //}


        //private static IMongoCollection<BsonDocument> GetMongoDbCollection(string collectionName)
        //{
        //    Console.WriteLine("Connect to localhost");
        //    const string connectionStirng = "mongodb://127.0.0.1:27017/?compressors=zlib&readPreference=primary&gssapiServiceName=mongodb&appname=MongoDB%20Compass%20Community&ssl=false";
        //    MongoClient client = new MongoClient(connectionStirng);

        //    Console.WriteLine($"Connect to database");
        //    database = client.GetDatabase("metadata");

        //    Console.WriteLine($"Connect to collection: {collectionName}");
        //    return database.GetCollection<BsonDocument>(collectionName);
        //}

        //private static IMongoCollection<BsonDocument> UpsertDocumentToCollection(BsonDocument hubWithScenes, IMongoCollection<BsonDocument> collection)
        //{
        //    var filter = Builders<BsonDocument>.Filter.Exists("hub");
        //    var result = collection.Find(filter).ToList();
        //    if (result.Count > 0)
        //    {
        //        database.DropCollection(collection.CollectionNamespace.CollectionName);
        //    }

        //    collection.InsertOne(hubWithScenes);
        //    Console.WriteLine($" ");
        //    return collection;
        //}
    }
}
