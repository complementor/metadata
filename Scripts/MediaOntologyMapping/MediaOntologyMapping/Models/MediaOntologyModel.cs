using System.Collections.Generic;

namespace MediaOntologyMapping.Models
{
    public class MediaOntologyModel
    {
        public List<object> ListOfProperties { get; set; }

        public MediaOntologyModel()
        {
            ListOfProperties =  new List<object>();
        }

    }
}
