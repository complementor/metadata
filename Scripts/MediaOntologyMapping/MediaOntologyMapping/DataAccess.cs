using Newtonsoft.Json.Linq;
using System.IO;
using System.Linq;

namespace MediaOntologyMapping
{
    public class DataAccess : IDataAccess
    {
        public JObject GetExifMetadataDeserialized()
        {
            JArray o1 = JArray.Parse(File.ReadAllText(@"P:\src\Metadata\mo\28e78611-8dc7-4c86-8d48-3187a0c2a659.json"));

            return (JObject)o1.First();
        }
    }
}
