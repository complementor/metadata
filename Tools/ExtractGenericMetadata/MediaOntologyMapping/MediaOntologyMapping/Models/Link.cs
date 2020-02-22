using System;

namespace MediaOntologyMapping.Models
{
    public class Link
    {
        public string Source { get; set; }
        public string id { get; set; }
        public Hub Hub { get; set; }
        public Hub OriginalHub { get; set; }
    }
}