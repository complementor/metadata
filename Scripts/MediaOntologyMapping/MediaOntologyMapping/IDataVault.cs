using MediaOntologyMapping.Models;
using Newtonsoft.Json.Linq;

namespace MediaOntologyMapping
{
    public interface IDataVault
    {
        DataVaultDocument CreateDocument(JObject original, MediaOntologyExifModel exif, 
            MediaOntologyDublinCoreModel dc, MediaOntologyXmpModel xmp, MediaOntologyId3Model id3);

        void WriteDocument(DataVaultDocument dataVaultStructure);
    }
}