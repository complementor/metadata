using MediaOntologyMapping.Models;

namespace MediaOntologyMapping
{
    class Program
    {
        static void Main(string[] args)
        {
            DataAccess dataAccess = new DataAccess();
            var original = dataAccess.GetExifMetadataDeserialized();

            OntologyForMediaResources mapping = new OntologyForMediaResources();
            MediaOntologyModel mediaOntologyModel = new MediaOntologyModel();
            mediaOntologyModel.ListOfProperties.AddRange(mapping.GetDublinCoreProperties(original));
            //MediaOntologyXmpModel xmp = mapping.GetMoXmpProperties(original);
            //MediaOntologyExifModel exif = mapping.GetExifProperties(original);
            //MediaOntologyId3Model id3 = mapping.GetMoId3Properties(original);

            DataVault dataVault = new DataVault();
            DataVaultDocument dataVaultStructure = dataVault.CreateDocument(original, mediaOntologyModel/*, exif, dc, xmp, id3*/);
            dataVault.WriteDocument(dataVaultStructure);


        }
    }
}
