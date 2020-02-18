# $source = "P:/resources/YoutubeToBeProcessed/cb2f75f3-83f8-499b-856a-539c4a73e78c.mp4"
$source = "P:/resources/YoutubeToBeProcessed/"

$destination = "P:/resources/YoutubeToBeProcessed/metadata/"

$files = Get-ChildItem -Path $source
ForEach ($file in $files) {
    python "P:/src/Scripts/scene-detector/detect_scenes.py" -v $file.FullName -o $destination
};    
# python "P:/src/Scripts/scene-detector/detect_scenes.py" -v $source -o $destination



