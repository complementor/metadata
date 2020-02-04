
$Source = "P:\src\Metadata\DataVaultDocumentCollection\"
$Files = Get-ChildItem -Path $Source
$count = 0
ForEach ($file in $Files) {
    if ($file.Extension -eq ".json") {
        $count++;
        Write-output("Import file nr " + $count + " : " + $file.FullName)
        & "C:\Program Files\MongoDB\Server\4.2\bin\mongoimport.exe" --db metadata --collection generic $file.FullName --jsonArray
    }
}; 


