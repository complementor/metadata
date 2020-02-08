using MediaOntologyMapping.Mappings;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MediaOntologyMapping
{
    public class OntologyForMediaResourcesMapper /*: IOntologyForMediaResources*/
    {
        private readonly JObject originalMetadata;

        public OntologyForMediaResourcesMapper(JObject originalMetadata)
        {
            this.originalMetadata = originalMetadata;
        }

        internal List<object> GetMediaOntologyProperties()
        {
            List<object> mediaOntologyProperties = new List<object>();

            mediaOntologyProperties.AddRange(GetDublinCoreProperties(originalMetadata));
            mediaOntologyProperties.AddRange(GetExifProperties(originalMetadata));
            mediaOntologyProperties.AddRange(GetXmpProperties(originalMetadata));
            mediaOntologyProperties.AddRange(GetId3Properties(originalMetadata));
            mediaOntologyProperties.AddRange(GetMpeg7Properties(originalMetadata));
            mediaOntologyProperties.AddRange(GetEBUCoreProperties(originalMetadata));
            mediaOntologyProperties.AddRange(GetIPTCProperties(originalMetadata));

            return mediaOntologyProperties;
        }

        public List<object> GetDublinCoreProperties(JObject original)
        {
            var dublinCoreProperties = new List<object>();
            var dublinCoreDictionary = Dictionaries.GetDublinCoreDictionary();
            foreach (var item in original)
            {
                var relatedDictionaryKey = dublinCoreDictionary.Where(d => d.Value.ToLowerInvariant().Replace(" ", "") == item.Key.ToLowerInvariant().Replace(" ", "")).FirstOrDefault().Key;
                if (relatedDictionaryKey != null)
                {
                    var jsonString = CreateJsonMediaOntologyProperty(relatedDictionaryKey,item.Value.ToString());
                    
                    dublinCoreProperties.Add(JObject.Parse(jsonString));
                }
            }

            return dublinCoreProperties;
        }

        public List<object> GetIPTCProperties(JObject original)
        {
            var iptcCoreProperties = new List<object>();
            var iptcDictionary = Dictionaries.GetIPTCDictionary();
            foreach (var item in original)
            {
                var relatedDictionaryKey = iptcDictionary.Where(d => d.Value.ToLowerInvariant().Replace(" ", "") == item.Key.ToLowerInvariant().Replace(" ", "")).FirstOrDefault().Key;
                if (relatedDictionaryKey != null)
                {
                    var jsonString = CreateJsonMediaOntologyProperty(relatedDictionaryKey, item.Value.ToString());
                    iptcCoreProperties.Add(JObject.Parse(jsonString));
                }
            }

            return iptcCoreProperties;
        }

        public List<object> GetEBUCoreProperties(JObject original)
        {
            var ebucoreProperties = new List<object>();
            var ebucoreDictionary = Dictionaries.GetEBUCoreDictionary();
            foreach (var item in original)
            {
                var relatedDictionaryKey = ebucoreDictionary.Where(d => d.Value.ToLowerInvariant().Replace(" ", "") == item.Key.ToLowerInvariant().Replace(" ", "")).FirstOrDefault().Key;
                if (relatedDictionaryKey != null)
                {
                    var jsonString = CreateJsonMediaOntologyProperty(relatedDictionaryKey, item.Value.ToString());
                    ebucoreProperties.Add(JObject.Parse(jsonString));
                }
            }

            return ebucoreProperties;
        }

        public List<object> GetMpeg7Properties(JObject original)
        {
            var mpeg7Properties = new List<object>();
            var mpeg7Dictionary = Dictionaries.GetMPEG7Dictionary();
            foreach (var item in original)
            {
                var relatedDictionaryKey = mpeg7Dictionary.Where(d => d.Value.ToLowerInvariant().Replace(" ", "") == item.Key.ToLowerInvariant().Replace(" ", "")).FirstOrDefault().Key;
                if (relatedDictionaryKey != null)
                {
                    var jsonString = CreateJsonMediaOntologyProperty(relatedDictionaryKey, item.Value.ToString());
                    mpeg7Properties.Add(JObject.Parse(jsonString));
                }
            }

            return mpeg7Properties;
        }

        public List<object> GetId3Properties(JObject original)
        {
            var id3Properties = new List<object>();
            var id3Dictionary = Dictionaries.GetId3Dictionary();
            foreach (var item in original)
            {
                var relatedDictionaryKey = id3Dictionary.Where(d => d.Value.ToLowerInvariant().Replace(" ", "") == item.Key.ToLowerInvariant().Replace(" ", "")).FirstOrDefault().Key;
                if (relatedDictionaryKey != null)
                {
                    var jsonString = CreateJsonMediaOntologyProperty(relatedDictionaryKey, item.Value.ToString());
                    id3Properties.Add(JObject.Parse(jsonString));
                }
            }

            return id3Properties;
        }

        public List<object> GetXmpProperties(JObject original)
        {
            var xmpProperties = new List<object>();
            var xmpDictionary = Dictionaries.GetXmpDictionary();
            foreach (var item in original)
            {
                var relatedDictionaryKey = xmpDictionary.Where(d => d.Value.ToLowerInvariant().Replace(" ", "") == item.Key.ToLowerInvariant().Replace(" ", "")).FirstOrDefault().Key;
                if (relatedDictionaryKey != null)
                {
                    var jsonString = CreateJsonMediaOntologyProperty(relatedDictionaryKey, item.Value.ToString());
                    xmpProperties.Add(JObject.Parse(jsonString));
                }
            }

            return xmpProperties;
        }

        public List<object> GetExifProperties(JObject original)
        {
            var exifProperties = new List<object>();
            var exifDictionary = Dictionaries.GetExifDictionary();
            foreach (var item in original)
            {
                var relatedDictionaryKey = exifDictionary.Where(d => d.Value.ToLowerInvariant().Replace(" ", "") == item.Key.ToLowerInvariant().Replace(" ", "")).FirstOrDefault().Key;
                if (relatedDictionaryKey != null)
                {
                    var jsonString = CreateJsonMediaOntologyProperty(relatedDictionaryKey, item.Value.ToString());
                    exifProperties.Add(JObject.Parse(jsonString));
                }
            }

            return exifProperties;
        }

        private string CreateJsonMediaOntologyProperty(string relatedDictionaryKey, string v)
        {
            var jsonString = @"{'" + HttpUtility.HtmlEncode(relatedDictionaryKey) + "': { 'Name': '" + HttpUtility.HtmlEncode(relatedDictionaryKey) + "', 'Value': '" + HttpUtility.HtmlEncode(v.Replace("\\", "")) + "', 'Standard': 'dc'  }}";
            jsonString = jsonString.Replace("'", "\"");
            return jsonString;
        }

    }
}
