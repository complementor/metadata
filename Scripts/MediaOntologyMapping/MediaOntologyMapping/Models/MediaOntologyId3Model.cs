using System;
using System.Collections.Generic;
using System.Text;

namespace MediaOntologyMapping.Models
{
    public class MediaOntologyId3Model
    {
        public MediaOntologyProperty RecordingTime { get; set; }
        public MediaOntologyProperty ReleaseTime { get; set; }
        public MediaOntologyProperty TaggingTime { get; set; }


        //perl $ExifTool -RecordingTime -ReleaseTime -TaggingTime -EncodedBy -Lyricist -InvolvedPeople -MusicianCredits -Conductor -InterpretedBy -Picture -Popularimeter -Album -Genre -Copyright -EncodingTime -FileType -FileType -Grouping -Title -Subtitle -Language -Length -Mood -Artist -Band -Publisher -charset utf8 -j -json $SourceFolder > $basicMetadataFilePathId3

    }
}
