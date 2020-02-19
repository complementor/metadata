#beware this will override the indexes creted so far
$generic = "P:\src\Metadata\GenericCollection.json"
& "C:\Program Files\MongoDB\Server\4.2\bin\mongoimport.exe" --db metadata --collection generic $generic --jsonArray

$features = "P:\src\Metadata\FeatureCollection.json"
& "C:\Program Files\MongoDB\Server\4.2\bin\mongoimport.exe" --db metadata --collection features $features --jsonArray