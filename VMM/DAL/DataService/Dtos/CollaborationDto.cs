using System.Collections.Generic;

namespace MongoDbAccessLayer.DataService.Dtos
{
    public class CollaborationDto
    {
        public List<Comment> Comments { get; set; }
        public List<Tag> Tags { get; set; }
    }

    public class Comment
    {
        public string User { get; set; }
        public string UserIcon { get; set; }
        public string Description { get; set; }
        public string DateTime { get; set; }
    }

    public class Tag
    {
        public string UserName { get; set; }
        public string TagName { get; set; }
        public string Datetime { get; set; }

    }
}
