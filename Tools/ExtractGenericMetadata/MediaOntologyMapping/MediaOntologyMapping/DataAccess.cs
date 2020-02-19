using Newtonsoft.Json.Linq;
using System.IO;
using System.Linq;

namespace MediaOntologyMapping
{
    public class DataAccess : IDataAccess
    {
        public JObject GetExifMetadataDeserialized(string filePath)
        {
            JArray o1 = JArray.Parse(File.ReadAllText(filePath));

            return (JObject)o1.First();
        }
    }
}
