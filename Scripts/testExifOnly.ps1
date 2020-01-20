$SourceFolder = "P:\samples\28e78611-8dc7-4c86-8d48-3187a0c2a659.avi"
$basicMetadataFilePath = "P:\src\GenericOntologyMapping\Metadata\exifOnly\28e78611-8dc7-4c86-8d48-3187a0c2a659.json"
$ExifTool = "C:\Programming\Image-ExifTool-11.78\Image-ExifTool-11.78\exiftool.pl"
perl $ExifTool -common -charset utf8 -j -json $SourceFolder > $basicMetadataFilePath