using System.Runtime.CompilerServices;
using System;
using System.ComponentModel.DataAnnotations;

namespace ViralLinks.Data
{
    public class PostCategory
    {
        [Key]
        public string Id { get; set; }
        public string Header { get; set; }
        public string SubHeader { get; set; }
        public string Description { get; set; }
        public int Position { get; set; }
    }

    public class Post
    {
        [Key]
        public string PostId { get; set; }
        public string CategoryId { get; set; }
        public string CategoryHeader { get; set; }
        public string UserId { get; set; }
        public string Username { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public string PostLink { get; set; }
        public DateTime TimeStamp { get; set; }
        public Post(){}

        public Post(ApplicationUser user,CreatePostModel postModel,PostCategory category)
        {
            this.PostId = Guid.NewGuid().ToString();
            this.CategoryId = category.Id;
            this.CategoryHeader = category.Header;
            this.UserId = user.Id;
            this.Username = user.UserName;
            this.Subject = postModel.Subject;
            this.Description = postModel.Description;
            this.PostLink = postModel.Link;
            this.TimeStamp = DateTime.Now;
        }

    }

    public class PostLinkVisits
    {
        [Key]
        public long Id { get; set; }
        public string PostId { get; set; }
        public string UserId { get; set; }
        public DateTime TimeStamp { get; set; }

        public PostLinkVisits(){}
        public PostLinkVisits(string userid, string postid)
        {
            this.PostId = postid;
            this.UserId = userid;
        }
    }

    public class PostComment
    {
        [Key]
        public long Id { get; set; }
        public string PostId { get; set; }
        public string Text { get; set; }
        public DateTime TimeStamp { get; set; }
        public string UserId { get; set; }

        public PostComment()
        {
            TimeStamp = DateTime.Now;
        }
        
    }

    public class PostCertificate
    {
        [Key]
        public long Id { get; set; }
        public string UserId { get; set; }
        public string PostId { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}