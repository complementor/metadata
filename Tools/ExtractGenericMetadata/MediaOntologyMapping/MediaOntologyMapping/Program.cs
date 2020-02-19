namespace MediaOntologyMapping
{
    class Program
    {
        static void Main(string[] args)
        {
            var source = @"P:\src\Metadata\OriginalMetadata\";
            var destination = @"P:\src\Metadata\DataVaultDocumentCollection\";
            int batch = 1000;
            
            MediaOntologyMappingBusinessLogic mediaOntologyMappingBusinessLogic = new MediaOntologyMappingBusinessLogic(source, destination, batch);
            mediaOntologyMappingBusinessLogic.Execute();
        }
    }
} 
