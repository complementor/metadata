$features = "P:\src\Metadata\FeatureCollection.json"
& "C:\Program Files\MongoDB\Server\4.2\bin\mongoimport.exe" --db metadata --collection features $features --jsonArray