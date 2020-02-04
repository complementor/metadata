using MediaOntologyMapping.Models;
using System;

namespace MediaOntologyMapping
{
    public class DataVaultDocument
    {
        public Guid Id { get; set; }
        public Hub Hub { get; set; }
        public Hub OriginalHub { get; set; }
    }
}