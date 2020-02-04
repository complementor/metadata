using System;
using System.Collections.Generic;
using System.Text;

namespace MediaOntologyMapping.Models
{
    public class MediaOntologyExifModel
    {
        public MediaOntologyProperty ImageWidth { get; set; }
        public MediaOntologyProperty ImageHeight { get; set; }
        public MediaOntologyProperty Compression { get; set; }

        //ImageWidth -ImageHeight -Compression -ImageUniqueID -ImageDescription -ITCH -ISRC -IENG -IART -IART -ISRC -DateTime -ICRD -DateTimeOriginal -GPSLatituteREF -comments -UserComment -IKEY -ISBJ -IGNR -RelatedSoundFile -Copyright  -GPSLongitudeREF -GPSLatitute -GPSLongitude -GPSAltitude -GPSAltitudeRef
    }
}
