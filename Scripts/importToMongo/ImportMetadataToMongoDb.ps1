$Source = "P:\samples\"
$jsonExtension = ".json"

$mongoImportPath = "C:\Program Files\MongoDB\Server\4.2\bin\mongoimport.exe"

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
            # perl "C:\Programming\Image-ExifTool-11.78\Image-ExifTool-11.78\exiftool.pl" -json P:\samples\ccd20a09-e1cf-4b52-a22c-108ca91ec220.jpg
            mongoimport.exe --db metadata --collection video P:\src\sampleExtraction\0e951022-5a19-410c-babd-ca4c5e6ee38d.json --jsonArray
            
        }
        catch {
            Write-output("it didn't run")
        }
};     

