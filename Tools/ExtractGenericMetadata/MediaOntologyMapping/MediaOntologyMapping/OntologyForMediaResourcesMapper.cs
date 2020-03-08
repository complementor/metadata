using MediaOntologyMapping.Mappings;
using MediaOntologyMapping.Models;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

namespace MediaOntologyMapping
{
    public class OntologyForMediaResourcesMapper /*: IOntologyForMediaResources*/
    {
        private readonly JObject originalMetadata;

        public OntologyForMediaResourcesMapper(JObject originalMetadata)
        {
            this.originalMetadata = originalMetadata;
        }

        internal List<OMRAttribute> GetMediaOntologyProperties()
        {
            List<OMRAttribute> mediaOntologyProperties = new List<OMRAttribute>();

            mediaOntologyProperties.AddRange(GetStandardProperties(originalMetadata, Dictionaries.GetDublinCoreDictionary(), "dc"));
            mediaOntologyProperties.AddRange(GetStandardProperties(originalMetadata, Dictionaries.GetExifDictionary(), "exif"));
            mediaOntologyProperties.AddRange(GetStandardProperties(originalMetadata, Dictionaries.GetXmpDictionary(), "xmp"));
            mediaOntologyProperties.AddRange(GetStandardProperties(originalMetadata, Dictionaries.GetId3Dictionary(), "id3"));
            mediaOntologyProperties.AddRange(GetStandardProperties(originalMetadata, Dictionaries.GetMPEG7Dictionary(), "mpeg7"));
            mediaOntologyProperties.AddRange(GetStandardProperties(originalMetadata, Dictionaries.GetEBUCoreDictionary(), "ebucore"));
            mediaOntologyProperties.AddRange(GetStandardProperties(originalMetadata, Dictionaries.GetIPTCDictionary(), "iptc"));
            
            var youtubeProperties = GetYouTubeProperties(originalMetadata, Dictionaries.GetYoutubeDictionary(), "youtube");
            if (youtubeProperties != null)
            {
                mediaOntologyProperties.AddRange(youtubeProperties);
            }

            return mediaOntologyProperties;
        }

        private IEnumerable<OMRAttribute> GetYouTubeProperties(JObject original, Dictionary<string, string> standardDictionary, string standard)
        {
            var youtubeProperty = new List<OMRAttribute>();
            foreach (var item in original)
            {
                var key = standardDictionary
                .Where(d => d.Value.ToLowerInvariant().Replace(" ", "") == item.Key.ToLowerInvariant().Replace(" ", ""))
                .FirstOrDefault().Key;

                if (key != null)
                {
                    var attr = new OMRAttribute()
                    {
                        Standard = standard,
                        Name = key,
                        Value = GetYouTubeID(item.Value.ToString())
                    };

                    if (attr.Value == null) return null; 

                    youtubeProperty.Add(attr);
                }
            }

            return youtubeProperty;
        }

        private string GetYouTubeID(string v)
        {
            var value = v.Split('.')[0];

            if (string.IsNullOrEmpty(value)) return null;
            if (value.Length != 11) return null;

            return value;
        }

        public List<OMRAttribute> GetStandardProperties(JObject original, Dictionary<string, string> standardDictionary, string standard)
        {
            var dublinCoreProperties = new List<OMRAttribute>();
            foreach (var item in original)
            {
                var key = standardDictionary
                .Where(d => d.Value.ToLowerInvariant().Replace(" ", "") == item.Key.ToLowerInvariant().Replace(" ", ""))
                .FirstOrDefault().Key;

                if (key != null)
                {
                    var attr = new OMRAttribute()
                    {
                        Standard = standard,
                        Name = key,
                        Value = item.Value.ToString()
                    };

                    dublinCoreProperties.Add(attr);
                }
            }

            return dublinCoreProperties;
       }
    }
}
