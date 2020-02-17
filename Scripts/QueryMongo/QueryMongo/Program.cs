using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Linq;

namespace QueryMongo
{
    internal static class Program
    {
        static IMongoDatabase database;
        static void Main(string[] args)
        {
            var mongoCollection = GetMongoDbCollection("test");

            ExtendDocument(mongoCollection, Samples.HubWithScenes, Samples.With1AlgmDoc, "optical_character_recognition");
            ExtendDocument(mongoCollection, Samples.With1AlgmDoc, Samples.With2AlgmDoc, "speech_recognition");
            ExtendDocument(mongoCollection, Samples.With2AlgmDoc, Samples.With3AlgmDoc, "sentiment_analysis");
            ExtendDocument(mongoCollection, Samples.With3AlgmDoc, Samples.With4AlgmDoc, "objects");
        }

        private static void ExtendDocument(IMongoCollection<BsonDocument> mongoCollection, BsonDocument initialDocument,
            BsonDocument extendedDocument, string indexName)
        {
            var builder = Builders<BsonDocument>.Filter;
            IMongoCollection<BsonDocument> collection = InitializeDatabase(initialDocument, mongoCollection);

            Func<FilterDefinition<BsonDocument>> filter = () => builder.Eq("hub.satellite.video_information.scenes", 50);

            Console.WriteLine(@"Apply the query:  SELECT * FROM document");
            var initialHub = collection.Find(filter.Invoke()).First().AsBsonDocument;
            Console.WriteLine($"Initial document : {initialHub}");

            collection.ReplaceOne(filter.Invoke(), extendedDocument);
            Console.WriteLine($" ");
            Console.WriteLine($"Document is extended with: {indexName}");
            Console.WriteLine($" ");

            Console.WriteLine(@"Apply the query: SELECT * FROM document");
            var documentAfterUpdate = collection.Find(filter.Invoke()).First().AsBsonDocument;
            Console.WriteLine($"Extended document: {documentAfterUpdate}");

            Console.ReadLine();
        }

        private static IMongoCollection<BsonDocument> GetMongoDbCollection(string collectionName)
        {
            Console.WriteLine("Connect to localhost");
            const string connectionStirng = "mongodb://127.0.0.1:27017/?compressors=zlib&readPreference=primary&gssapiServiceName=mongodb&appname=MongoDB%20Compass%20Community&ssl=false";
            MongoClient client = new MongoClient(connectionStirng);

            Console.WriteLine($"Connect to database");
            database = client.GetDatabase("metadata");

            Console.WriteLine($"Connect to collection: {collectionName}");
            return database.GetCollection<BsonDocument>(collectionName);
        }

        private static IMongoCollection<BsonDocument> InitializeDatabase(BsonDocument hubWithScenes, IMongoCollection<BsonDocument> collection)
        {
            var filter = Builders<BsonDocument>.Filter.Exists("hub");
            var result = collection.Find(filter).ToList();
            if (result.Count > 0)
            {
                database.DropCollection(collection.CollectionNamespace.CollectionName);
            }

            collection.InsertOne(hubWithScenes);
            Console.WriteLine($" ");
            return collection;
        }
    }
}