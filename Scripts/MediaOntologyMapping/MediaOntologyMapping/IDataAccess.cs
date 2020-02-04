using Newtonsoft.Json.Linq;

namespace MediaOntologyMapping
{
    public interface IDataAccess
    {
        JObject GetExifMetadataDeserialized(string filePath);
    }
}