using MediaOntologyMapping.Mappings;
using MediaOntologyMapping.Models;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

namespace MediaOntologyMapping
{
    public class OntologyForMediaResources /*: IOntologyForMediaResources*/
    {
        public List<object> GetDublinCoreProperties(JObject original)
        {
            var dublinCoreProperties = new List<object>();
            var dublinCoreDictionary = Dictionaries.GetDublinCoreDictionary();
            foreach (var item in original)
            {
                var relatedDictionaryKey = dublinCoreDictionary.Where(d => d.Value.ToLowerInvariant().Replace(" ", "") == item.Key.ToLowerInvariant().Replace(" ", "")).FirstOrDefault().Key;
                if (relatedDictionaryKey != null)
                {
                    var jsonString = @"{'" + relatedDictionaryKey + "': { 'Name': '" + relatedDictionaryKey + "', 'Value': '" + item.Value.ToString() + "', 'Standard': 'dc'  }}";
                    jsonString =  jsonString.Replace("'", "\"");
                    dublinCoreProperties.Add(JObject.Parse(jsonString));
                }

            }

            return dublinCoreProperties;
        }

        //public MediaOntologyExifModel GetExifProperties(JObject original)
        //{
        //    throw new NotImplementedException();
        //}

        //public MediaOntologyId3Model GetMoId3Properties(JObject original)
        //{
        //    throw new NotImplementedException();
        //}

        //public MediaOntologyXmpModel GetMoXmpProperties(JObject original)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
