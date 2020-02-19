$source = "P:/resources/YoutubeToBeProcessed/"
$destination = "P:/resources/YoutubeToBeProcessed/metadata/"

$files = Get-ChildItem -Path $source
ForEach ($file in $files) {
    Write-Host $file.FullName
    Write-Host $destination

    python "P:/src/Scripts/scene-detector/detect_scenes.py" -v $file.FullName -o $destination
};    
# $source = "P:/resources/YoutubeToBeProcessed/cb2f75f3-83f8-499b-856a-539c4a73e78c.mp4"
# python "P:/src/Scripts/scene-detector/detect_scenes.py" -v "P:\resources\YoutubeToBeProcessed\" -o "P:\resources\YoutubeToBeProcessed\metadata\"
# "P:\resources\YoutubeToBeProcessed\"
# "P:/resources/YoutubeToBeProcessed/"
# "P:\resources\YoutubeToBeProcessed\metadata\"
# "P:/resources/YoutubeToBeProcessed/metadata/"
