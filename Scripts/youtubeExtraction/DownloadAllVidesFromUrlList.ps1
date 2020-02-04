$source = "P:\resources\1000links.txt"
$destination = "p:\resources\youtube\"

ForEach ($url in Get-Content $source) {
    if ($url -match $regex) {
        $guid = [guid]::NewGuid().ToString();
        & "C:\Programming\FFmpeg\bin\youtube-dl.exe" --add-metadata $Url -o $destination$guid
    }
}
