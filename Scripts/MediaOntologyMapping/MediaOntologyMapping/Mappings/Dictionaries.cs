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

        public static Dictionary<string, string> GetIPTCDictionary()
        {
            var iptcDictionary = new Dictionary<string, string>
            {
                { "frameRate", "VideoFrameRate" },
                { "averageBitRate", "AvgBitrate" },
                { "samplingRate", "AudioSampleRate" },
            };

            return iptcDictionary;
        }

        public static Dictionary<string, string> GetMPEG7Dictionary()
        {
            var mpeg7Dictionary = new Dictionary<string, string>
            {
                { "numTracks", "AudioChannels" },
                { "duration", "MediaDuration" },
            };

            return mpeg7Dictionary;
        }

        public static Dictionary<string, string> GetEBUCoreDictionary()
        {
            var ebucoreDictionary = new Dictionary<string, string>
            {
                { "format", "MIMEType" },
                { "numTracks", "AudioFormat" },
                { "compression", "AudioFormat" },
            };

            return ebucoreDictionary;
        }

        public static Dictionary<string, string> GetExifDictionary()
        {
            var exifDictionary = new Dictionary<string, string>
            {
                //{ "identifier", "ImageDescription" },
                //{ "identifier", "INAM" },
                { "identifier", "Title" },
                //{ "contributor", "IART"},
                //{ "contributor", "Artist"},
                //{ "contributor", "IENG"}, 
                //{ "contributor", "Engineer"}, 
                //{ "contributor",  "ISRC"}, 
                //{ "contributor",  "Source"}, 
                //{ "contributor", "ITCH" },
                //{ "contributor", "Technician" },
                //{ "creator", "IART" },
                { "creator", "Artist" },
                //{ "creator", "ISRC" },
                //{ "creator", "Source" },
                //{ "creator", "creator" },
                { "date", "DateTime" },
                //{ "date", "DateTimeOriginal" },
                //{ "date", "ICRD" },
                //{ "date", "DateCreated" },
                //{ "location", "GPSLatituteREF" },
                { "locationLatitute", "GPSLatitute" },
                //{ "location", " GPSLongitudeREF" },
                { "locationLongitude", "GPSLongitude" },
                //{ "location", "GPSLongitude" },
                //{ "location", "GPSAltitudeRef" },
                //{ "description", "IKEY" },
                //{ "description", "Keywords" },
                //{ "description", "UserComment" },
                { "description", "Comments" },
                //{ "keyword", "ISBJ" },
                { "keyword", "Subject" },
                //{ "genre", "IGNR" },
                { "genre", "Genre" },
                { "relation", "RelatedSoundFile" },
                { "copyright", "Copyright" },
                { "frameSizeWidth", "ImageWidth" },
                { "frameSizeHeight", "ImageHeight" },
                { "compression", "Compression" },
            };

            return exifDictionary;
        }

        public static Dictionary<string, string> GetId3Dictionary()
        {
            var id3Dictionary = new Dictionary<string, string>
            {
                //{ "title", "TIT2" },
                //{ "title", "Title" },
                //{ "title", "TIT3" },
                //{ "title", "Subtitle" },
                //{ "language", "TLAN" },
                { "language", "Language" },
                //{ "contributor", "TPE2" },
                { "contributor", "Band" },
                //{ "contributor", "TPE3" },
                //{ "contributor", "Conductor" },
                //{ "contributor", "TPE4" },
                //{ "contributor", "InterpretedBy" },
                //{ "contributor", "TEXT" },
                //{ "contributor", "Lyricist" },
                //{ "contributor", "TMCL" },
                //{ "contributor", "MusicianCredits" },
                //{ "contributor", "TIPL" },
                //{ "contributor", "InvolvedPeople" },
                //{ "contributor", "TENC" },
                //{ "contributor", "EncodedBy" },
                //{ "creator", "TPE1" },
                { "creator", "Artist" },
                //{ "date", "TDEN" },
                { "date", "EncodingTime" },
                //{ "date", "TDRC" },
                //{ "date", "RecordingTime" },
                //{ "date", "TDRL" },
                //{ "date", "ReleaseTime" },
                //{ "date", "TDTG" },
                //{ "date", "TaggingTime" },
                //{ "description", "TIT1" },
                { "description", "Grouping" },
                //{ "keyword", "TMOO" },
                { "keyword", "Mood" },
                //{ "genre", "TCON" },
                { "genre", "Genre" },
                //{ "rating", "POPM" },
                { "rating", "Popularimeter" },
                //{ "relation", "APIC" },
                { "relation", "Picture" },
                //{ "collection", "TALB" },
                { "collection", "Album" },
                //{ "copyright", "TCOP" },
                { "copyright", "Copyright" },
                //{ "publisher", "TPUB" },
                { "publisher", "Publisher" },
                //{ "compression", "TFLT" },
                { "compression", "FileType" },
                //{ "duration", "TLEN" },
                { "duration", "Length" },
                //{ "format", "TFLT" },
                { "format", "FileType" },
            };

            return id3Dictionary;
        }

        public static Dictionary<string, string> GetXmpDictionary()
        {
            var xmpDictionary = new Dictionary<string, string>
            {
                { "identifier", "identifier" },
                //{ "title", "title" },
                //{ "title", "album" },
                { "language", "language" },
                //{ "contributor", "contributor" },
                { "contributor", "artist" },
                //{ "contributor", "composer" },
                { "creator", "creator" },
                { "date", "CreateDate" },
                //{ "date", "DateCreated" },
                //{ "date", "DateCreated" },
                //{ "date", "date" },
                //{ "date", "ModifyDate" },
                { "locationLatitude", "GPSLatitude" },
                { "locationLongitude", "GPSLongitude" },
                { "location", "Country" },
                //{ "location", "City" },
                //{ "location", "State" },
                { "description", "description" },
                { "keyword", "subject" },
                { "genre", "genre" },
                { "rating", "Rating" },
                { "relation", "relation" },
                //{ "relation", "DerivedFrom" },
                //{ "relation", "History" },
                //{ "relation", "Ingredients" },
                { "copyright", "rights" },
                { "policy", "Certificate" },
                //{ "policy", "UsageTerms" },
                //{ "policy", "WebStatement" },
                { "publisher", "publisher" },
                { "frameSize", "videoFrameSize" },
                { "compression", "Compression" },
                //{ "compression", "audioCompressor" },
                { "duration", "duration" },
                { "format", "format" },
                { "audioSampleRate", "audioSampleRate" },
                { "numTracks", "trackNumber" },
            };

            return xmpDictionary;
        }
    }
}
