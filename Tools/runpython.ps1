$source = "P:/resources/YoutubeToBeProcessed/"
$destination = "P:/Metadata/FeatureAnnotationMetadata/"

$files = Get-ChildItem -Path $source
ForEach ($file in $files) {
    Write-Host $file.FullName
    Write-Host $destination
    python "P:/src/Tools/ExtractFeaturesMetadata/detect_scenes.py" -v $file.FullName -o $destination
};    
