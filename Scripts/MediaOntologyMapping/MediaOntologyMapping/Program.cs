namespace MediaOntologyMapping
{
    class Program
    {
        static void Main(string[] args)
        {
            var source = @"P:\src\Metadata\OriginalMetadata\";
            var destination = @"P:\src\Metadata\DataVaultDocumentCollection\";
            
            MediaOntologyMappingBusinessLogic mediaOntologyMappingBusinessLogic = new MediaOntologyMappingBusinessLogic(source, destination);
            mediaOntologyMappingBusinessLogic.Execute();
        }
    }
} 
