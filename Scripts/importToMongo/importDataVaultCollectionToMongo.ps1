$fileFullName = "P:\src\Metadata\DataVaultDocumentCollection\MongoImport.json"
& "C:\Program Files\MongoDB\Server\4.2\bin\mongoimport.exe" --db metadata --collection links $fileFullName --jsonArray