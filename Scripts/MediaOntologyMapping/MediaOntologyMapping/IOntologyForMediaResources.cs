using MediaOntologyMapping.Models;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace MediaOntologyMapping
{
    public interface IOntologyForMediaResources
    {
        List<MediaOntologyProperty> GetDublinCoreProperties(JObject original);
        //MediaOntologyExifModel GetExifProperties(JObject original);
        //MediaOntologyXmpModel GetMoXmpProperties(JObject original);
        //MediaOntologyId3Model GetMoId3Properties(JObject original);
    }
}