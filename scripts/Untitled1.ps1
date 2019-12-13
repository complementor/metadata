# //add event listener 
# //set parameters 
$Source = "P:\samples\"
$jsonExtension = ".json"

$files = Get-ChildItem -Path $Source
ForEach ($file in $files) {
    $guid2 = [guid]::NewGuid()
    $guid = $guid2.ToString();
    # create json 
    $metadataPath = $directory + $guid + $jsonExtension
    New-Item $metadataPath -ItemType file 
    Write-output("New Json file created: " + $metadataPath)

    Write-Output ($file.BaseName + " will be renamed to : " + $guid + $file.Extension)
    $newVideoFileName = $guid + $file.Extension
    Rename-Item $file.FullName -NewName $newVideoFileName 

    $newFilePath = $Source + $newVideoFileName
    try {

        perl "C:\Programming\Image-ExifTool-11.78\Image-ExifTool-11.78\exiftool.pl" -j -json $newFilePath >$metadataPath

    }
    catch {
        Write-output("it didn't run")
    }
};     