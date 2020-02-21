
Param(
    $SourceFolder,
    $DestinationFolder,
    $ExifTool
)

# $SourceFolder = "P:\resources\YoutubeToBeProcessed\"
# $DestinationFolder = "P:\Metadata\OriginalExifMetadata\"
# $ExifTool = "C:\Programming\Image-ExifTool-11.78\Image-ExifTool-11.78\exiftool.pl"
#wipe out destination folder


Get-ChildItem -Path $DestinationFolder -Recurse -File | Remove-Item

#extract exif metadata, then give guid name to both file and sidecar
$files = Get-ChildItem -Path $SourceFolder
ForEach ($file in $files) {
    if ($file.Extension -ne ".json") {
       
        # $guid = [guid]::NewGuid().ToString();
       
        #Create Json file with basic metadata
        $basicMetadataFilePath = $DestinationFolder + $file.BaseName + ".json"
        New-Item $basicMetadataFilePath -ItemType file 

        #Create new json
        # #Rename the video with guid
        # $newVideoFileName = $guid + $file.Extension
        # Rename-Item $file.FullName -NewName $newVideoFileName 
        # $newVideoFilePath = $SourceFolder + $newVideoFileName

        perl $ExifTool -charset utf8 -j -json $file.FullName > $basicMetadataFilePath
    }
};



# perl "C:\Programming\Image-ExifTool-11.78\Image-ExifTool-11.78\exiftool.pl" "P:\resources\YoutubeToBeProcessed\b517ae5d-3334-4455-b25b-d88ba5629941.mkv"
# perl "C:\Programming\Image-ExifTool-11.78\Image-ExifTool-11.78\exiftool.pl" -rights="Andrei" "P:\resources\YoutubeToBeProcessed\b517ae5d-3334-4455-b25b-d88ba5629941.mkv"
