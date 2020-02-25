using System;

namespace MongoDbAccessLayer.Dtos
{
    public class Comment
    {
        public string User { get; }
        public string UserIcon { get;}
        public string Description { get; }
        public DateTime DateTime { get; }
    }
}