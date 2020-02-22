using MediaOntologyMapping.Models;
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

        public MediaOntologyMappingBusinessLogic(string source, string destination, int batch)
        {
            this.source = source;
            this.destination = destination;
            this.batch = batch;
        }

        internal void Execute()
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
                List<Models.Attribute> mediaOntologyProperties = ontologyForMediaResourcesMapper.GetMediaOntologyProperties();

                dataVaultLinkCollection.Add(dataVault.CreateLink(originalMetadata, mediaOntologyProperties));
                
            }
            dataVault.WriteJsonFile(dataVaultLinkCollection, destination);
        }
    }
}