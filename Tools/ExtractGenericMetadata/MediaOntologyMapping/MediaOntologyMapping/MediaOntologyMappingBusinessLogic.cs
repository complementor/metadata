using MediaOntologyMapping.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MediaOntologyMapping
{
    internal class MediaOntologyMappingBusinessLogic
    {
        private readonly string source;
        private readonly string destination;
        private readonly int batch;

        static IMongoCollection<Link> documentCollection;

        public MediaOntologyMappingBusinessLogic(string source, string destination, int batch)
        {
            this.source = source;
            this.destination = destination;
            this.batch = batch;
        }

        internal void Execute()
        {
            List<Link> dataVaultLinkCollection = CreateLink();

            //InitializeModel();

            ////var batches = (int)dataVaultLinkCollection.Count() / batch;
            //foreach (var item in dataVaultLinkCollection.Batch(batch))
            //{ 
            //    documentCollection.InsertMany(dataVaultLinkCollection);
            //}
        }

        private List<Link> CreateLink()
        {
            DirectoryInfo d = new DirectoryInfo(source);
            FileInfo[] Files = d.GetFiles("*.json");

            List<Link> dataVaultLinkCollection = new List<Link>();

            DataVault dataVault = new DataVault();
            foreach (var file in Files.Where(f => f.Length > 0).Select(f => new { f.FullName, f.Name }))
            {
                DataAccess dataAccess = new DataAccess();
                var originalMetadata = dataAccess.GetExifMetadataDeserialized(file.FullName);

                OntologyForMediaResourcesMapper ontologyForMediaResourcesMapper = new OntologyForMediaResourcesMapper(originalMetadata);
                List<OMRAttribute> mediaOntologyProperties = ontologyForMediaResourcesMapper.GetMediaOntologyProperties();

                dataVaultLinkCollection.Add(dataVault.CreateLink(originalMetadata, mediaOntologyProperties));

            }

            dataVault.WriteJsonFile(dataVaultLinkCollection, destination);

            return dataVaultLinkCollection;
        }

        private static void InitializeModel()
        {
            Console.WriteLine("Connect to localhost");
            const string connectionStirng = "mongodb://127.0.0.1:27017/?compressors=zlib&readPreference=primary&gssapiServiceName=mongodb&appname=MongoDB%20Compass%20Community&ssl=false";
            MongoClient client = new MongoClient(connectionStirng);
            Console.WriteLine($"Connect to database");
            IMongoDatabase context = client.GetDatabase("metadata");
            documentCollection = context.GetCollection<Link>("document");

        }
         
    }
    public static class MyExtensions
    {
        public static IEnumerable<IEnumerable<T>> Batch<T>(this IEnumerable<T> items,
                                                           int maxItems)
        {
            return items.Select((item, inx) => new { item, inx })
                        .GroupBy(x => x.inx / maxItems)
                        .Select(g => g.Select(x => x.item));
        }
    }
}