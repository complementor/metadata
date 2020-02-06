using MediaOntologyMapping.Models;
using System.IO;
using System.Linq;

namespace MediaOntologyMapping
{
    internal class MediaOntologyMappingBusinessLogic
    {
        private readonly string source;
        private readonly string destination;

        public MediaOntologyMappingBusinessLogic(string source, string destination)
        {
            this.source = source;
            this.destination = destination;
        }

        internal void Execute()
        {        
            DirectoryInfo d = new DirectoryInfo(source);
            FileInfo[] Files = d.GetFiles("*.json"); 

            foreach (var file in Files.Where(f => f.Length > 0).Select(f => new { f.FullName, f.Name }))
            {
                DataAccess dataAccess = new DataAccess();
                var originalMetadata = dataAccess.GetExifMetadataDeserialized(file.FullName);

                OntologyForMediaResources mapping = new OntologyForMediaResources();
                MediaOntologyModel mediaOntologyModel = new MediaOntologyModel();
                mediaOntologyModel.ListOfProperties.AddRange(mapping.GetDublinCoreProperties(originalMetadata));
                mediaOntologyModel.ListOfProperties.AddRange(mapping.GetExifProperties(originalMetadata));
                mediaOntologyModel.ListOfProperties.AddRange(mapping.GetXmpProperties(originalMetadata));
                mediaOntologyModel.ListOfProperties.AddRange(mapping.GetId3Properties(originalMetadata));
                mediaOntologyModel.ListOfProperties.AddRange(mapping.GetMpeg7Properties(originalMetadata));
                mediaOntologyModel.ListOfProperties.AddRange(mapping.GetEBUCoreProperties(originalMetadata));
                mediaOntologyModel.ListOfProperties.AddRange(mapping.GetIPTCProperties(originalMetadata));

                DataVault dataVault = new DataVault();
                DataVaultDocument dataVaultStructure = dataVault.CreateDocument(originalMetadata, mediaOntologyModel);
                dataVault.WriteDocument(dataVaultStructure, destination, file.Name);
            }
        }
    }
}