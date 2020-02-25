using System;
namespace MongoDbAccessLayer.Dtos
{
    public class CollaborationDto
    {
        public List<Comment> Coments { get; set; }
        public List<Tag> Tags { get; set; }
    }
}
