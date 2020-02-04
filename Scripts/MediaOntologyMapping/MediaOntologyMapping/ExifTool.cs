using Newtonsoft.Json.Linq;

namespace MediaOntologyMapping
{
    public class ExifTool
    {

        public ExifTool()
        {

        }

        public JObject GetOriginalMetadata(string SourcePath) {

            //perl $ExifTool - charset utf8 - j - json $SourceFolder > $basicMetadataFilePath

            return null;
        }
    }
}