namespace MediaOntologyMapping
{
    class Program
    {
        static string defaultSource = @"P:\Metadata\OriginalExifMetadata\";
        static string defaultDestination = @"P:\Metadata\GenericCollection.json";

        static void Main(string[] args)
        {
            if (args.Length == 1)
            {
                if (!string.IsNullOrEmpty(args[0]))
                {
                    defaultSource = args[0];
                }
                if (!string.IsNullOrEmpty(args[0]))
                {
                    defaultDestination = args[1];
                }
            }
            
            const int batch = 1000;

            MediaOntologyMappingBusinessLogic mediaOntologyMappingBusinessLogic = new MediaOntologyMappingBusinessLogic(defaultSource, defaultDestination, batch);
            mediaOntologyMappingBusinessLogic.Execute();
        }
    }
} 
