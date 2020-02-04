using System;
using System.Collections.Generic;
using System.Text;

namespace MediaOntologyMapping.Models
{
    public class MediaOntologyDublinCoreModel
    {
        public MediaOntologyProperty ImageWidth { get; set; }
        public MediaOntologyProperty ImageHeight { get; set; }
        public MediaOntologyProperty Compression { get; set; }

        //perl $ExifTool -contributor -coverage -creator -date -description -format -identifier -language -publisher -relation -rights -source -subject -title -type -charset utf8 -j -json $SourceFolder > $basicMetadataFilePathDC

    }
}
