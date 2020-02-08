using System;

namespace MediaOntologyMapping.Models
{
    public class Link
    {
        public Guid Id { get; set; }
        public Hub Hub { get; set; }
        public Hub OriginalHub { get; set; }
    }
}