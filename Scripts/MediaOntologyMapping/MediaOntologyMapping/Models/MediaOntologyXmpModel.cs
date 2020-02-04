using System;
using System.Collections.Generic;
using System.Text;

namespace MediaOntologyMapping.Models
{
    public class MediaOntologyXmpModel
    {
        public MediaOntologyProperty Duration { get; set; }
        public MediaOntologyProperty AudioSampleRate { get; set; }
        public MediaOntologyProperty CreateDate { get; set; }
        public MediaOntologyProperty ModifyDate { get; set; }

        //perl $ExifTool -Identifier -album -artist -composer -CreateDate -ModifyDate -genre -Rating -DerivedFrom -History -Ingredients -Certificate -UsageTerms -WebStatement -videoFrameSize -audioCompressor -duration -audioSampleRate -frameRate -trackNumber -charset utf8 -j -json $SourceFolder > $basicMetadataFilePathXMP

    }
}
