using System;
using System.Collections.Generic;

namespace MongoDbAccessLayer.Dtos
{
    public class CollaborationDto
    {
        public List<Comment> Comments { get; set; }
        public List<Tag> Tags { get; set; }
    }
}
