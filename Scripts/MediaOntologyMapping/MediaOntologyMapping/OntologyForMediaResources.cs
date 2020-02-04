using MediaOntologyMapping.Mappings;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
                    var jsonString = @"{'" + HttpUtility.HtmlEncode(relatedDictionaryKey) + "': { 'Name': '" + HttpUtility.HtmlEncode(relatedDictionaryKey) + "', 'Value': '" + HttpUtility.HtmlEncode(item.Value.ToString()) + "', 'Standard': 'dc'  }}";
                    jsonString =  jsonString.Replace("'", "\"");
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
                    var jsonString = @"{'" + HttpUtility.HtmlEncode(relatedDictionaryKey) + "': { 'Name': '" + HttpUtility.HtmlEncode(relatedDictionaryKey) + "', 'Value': '" + HttpUtility.HtmlEncode(item.Value.ToString()) + "', 'Standard': 'iptc'  }}";
                    jsonString = jsonString.Replace("'", "\"");
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
                    var jsonString = @"{'" + HttpUtility.HtmlEncode(relatedDictionaryKey) + "': { 'Name': '" + HttpUtility.HtmlEncode(relatedDictionaryKey) + "', 'Value': '" + HttpUtility.HtmlEncode(item.Value.ToString()) + "', 'Standard': 'ebucore'  }}";
                    jsonString = jsonString.Replace("'", "\"");
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
                    var jsonString = @"{'" + HttpUtility.HtmlEncode(relatedDictionaryKey) + "': { 'Name': '" + HttpUtility.HtmlEncode(relatedDictionaryKey) + "', 'Value': '" + HttpUtility.HtmlEncode(item.Value.ToString()) + "', 'Standard': 'mpeg7'  }}";
                    jsonString = jsonString.Replace("'", "\"");
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
                    var jsonString = @"{'" + HttpUtility.HtmlEncode(relatedDictionaryKey) + "': { 'Name': '" + HttpUtility.HtmlEncode(relatedDictionaryKey) + "', 'Value': '" + HttpUtility.HtmlEncode(item.Value.ToString()) + "', 'Standard': 'id3'  }}";
                    jsonString = jsonString.Replace("'", "\"");
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
                    var jsonString = @"{'" + HttpUtility.HtmlEncode(relatedDictionaryKey) + "': { 'Name': '" + HttpUtility.HtmlEncode(relatedDictionaryKey) + "', 'Value': '" + HttpUtility.HtmlEncode(item.Value.ToString()) + "', 'Standard': 'xmp'  }}";
                    jsonString = jsonString.Replace("'", "\"");
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
                    var jsonString = @"{'" + HttpUtility.HtmlEncode(relatedDictionaryKey) + "': { 'Name': '" + HttpUtility.HtmlEncode(relatedDictionaryKey) + "', 'Value': '" + HttpUtility.HtmlEncode(item.Value.ToString()) + "', 'Standard': 'exif'  }}";
                    jsonString = jsonString.Replace("'", "\"");
                    exifProperties.Add(JObject.Parse(jsonString));
                }
            }

            return exifProperties;
        }


       
    }
}
