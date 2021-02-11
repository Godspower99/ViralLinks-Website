using System;

namespace ViralLinks.Data
{
    public class PostCategory
    {
        public string Id { get; set; }
        public string Header { get; set; }
        public string SubHeader { get; set; }
        public string Description { get; set; }
        public int Position { get; set; }
    }

    public class Post
    {
        public string Id { get; set; }
        public string CategoryId { get; set; }
        public string UserId { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public string PostLink { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}