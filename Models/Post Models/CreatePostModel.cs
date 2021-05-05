using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Http;
using ViralLinks.Data;

namespace ViralLinks
{
    public class CreatePostModel : BasePageModel
    {
        public string Subject { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public string CategoryId { get; set; }
        public IFormFile Image { get; set; }

        public int SubjectCount => string.IsNullOrWhiteSpace(Subject) ? 0 : Subject.Count();
        public int DescriptionCount => string.IsNullOrWhiteSpace(Description) ? 0 : Description.Count();
        public CreatePostModel(ApplicationUser user, string profilePicture):base(user,profilePicture){}
        public CreatePostModel(ApplicationUser user):base(user){}
        public CreatePostModel(string profilePicture):base(profilePicture){}
        public CreatePostModel(){}
    }

    public class EditPostModel : BasePageModel
    {
        public string Subject { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public string CategoryId { get; set; }
        public IFormFile Image { get; set; }
        public string PostID { get; set; }
        public Post CurrentPost {get; set;}

        public EditPostModel(ApplicationUser user, string profilePicture, Post post):base(user,profilePicture)
        {
            CurrentPost = post;
            Subject = post.Subject;
            Description = post.Description;
            Link = post.PostLink;
            CategoryId = post.CategoryId;
            PostID = post.PostId;
        }

        public EditPostModel(ApplicationUser user):base(user){}
        public EditPostModel(string profilePicture):base(profilePicture){}
        public EditPostModel(){}
    }
}