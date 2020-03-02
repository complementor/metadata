using Microsoft.Extensions.Options;

namespace MongoDbAccessLayer.Context
{
    public class MongoSettings 
    {
        public string Connection { get; set; }
        public string DatabaseName { get;  set; }
    }
}