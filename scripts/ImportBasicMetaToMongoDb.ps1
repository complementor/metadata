$Source = "P:\src\sampleExtraction\"
$Files = Get-ChildItem -Path $Source

ForEach ($file in $Files) {
    if ($file.Extension -eq ".json") {
        Write-output("Import file: " + $file.FullName)
        & "C:\Program Files\MongoDB\Server\4.2\bin\mongoimport.exe" --db metadata --collection basic $file.FullName --jsonArray
    }
}; 