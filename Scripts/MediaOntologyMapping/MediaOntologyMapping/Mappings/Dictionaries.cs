using System.Collections.Generic;

namespace MediaOntologyMapping.Mappings
{
    public class Dictionaries
    {
        public static Dictionary<string, string> GetDublinCoreDictionary()
        {
            var dcDictionary = new Dictionary<string, string>
            {
                { "identifier", "identifier" },
                { "title", "title" },
                { "language", "language" },
                { "contributor", "contributor" },
                { "creator", "creator" },
                { "date", "date" },
                { "description", "description" },
                { "keyword", "subject" },
                { "genre", "type" },
                { "relation", "relation" },
                { "collection", "source" },
                { "copyright", "rights" },
                { "publisher", "publisher" },
                { "format", "format" },
            };
             
            return dcDictionary;
        }
        
    }
}
