
$SourceFolder = "P:\samples\"
$DestinationFolder = "P:\src\sampleExtraction\"
$ExifTool = "C:\Programming\Image-ExifTool-11.78\Image-ExifTool-11.78\exiftool.pl"

$files = Get-ChildItem -Path $SourceFolder
ForEach ($file in $files) {
    if ($file.Extension -ne ".json") {
       
        $guid = [guid]::NewGuid().ToString();
       
        #Create Json file with basic metadata
        $basicMetadataFilePath = $DestinationFolder + $guid + ".json"
        New-Item $basicMetadataFilePath -ItemType file 

        # Rename the video with guid
        $newVideoFileName = $guid + $file.Extension
        Rename-Item $file.FullName -NewName $newVideoFileName 

        $newVideoFilePath = $SourceFolder + $newVideoFileName
        perl $ExifTool -charset utf8 -j -json $newVideoFilePath > $basicMetadataFilePath
    }
};     

