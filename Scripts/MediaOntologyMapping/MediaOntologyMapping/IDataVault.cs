using MediaOntologyMapping.Models;
using Newtonsoft.Json.Linq;

namespace MediaOntologyMapping
{
    public interface IDataVault
    {
        DataVaultDocument CreateDocument(JObject original, MediaOntologyModel mediaOntologyModel);
        void WriteDocument(DataVaultDocument dataVaultStructure, string destinationFolder, string fileName);
    }
}