$destination = "p:\resources\youtube\"
$file = "p:\resources\youtube\1000links.txt"

ForEach ($url in Get-Content $file) {
    if ($url -match $regex) {
        $guid = [guid]::NewGuid().ToString();
        & "C:\Programming\FFmpeg\bin\youtube-dl.exe" --add-metadata $Url -o $destination$guid
    }
}
