$Utf8NoBomEncoding = New-Object System.Text.UTF8Encoding($False)
$files = Get-ChildItem -Path $Source
ForEach ($i in $files) {
    $dest = $i.Fullname.Replace($PWD, "P:\src\sampleExtraction\new\")

    if (!(Test-Path $(Split-Path $dest -Parent))) {
        New-Item $(Split-Path $dest -Parent) -type Directory
    }
    Write-Output ("I : " + $i.FullName)

    $content = Get-Content $i.FullName 
    Write-Output ("Content : " + $content)
    [System.IO.File]::WriteAllLines($dest, $content, $Utf8NoBomEncoding)
}