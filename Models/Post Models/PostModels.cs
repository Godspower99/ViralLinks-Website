using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ViralLinks.Data;

namespace ViralLinks.Models
{
    public class PostObjectModel : BasePageModel
    {
        public Post Post { get; set; }
        public string ImageURI { get; set; }
        public string UserImageURI { get; set; }
        public int Visits { get; set; }
        public int CommentsCount { get; set; }
        public Tuple<bool,int> PostCertificates { get; set; }



        public PostObjectModel(){}
        public PostObjectModel(ApplicationUser user):base(user){}
        public PostObjectModel(Post post, string imageUri, string userImageUri)
        {
            this.Post = post;
            this.ImageURI = imageUri;
            this.UserImageURI = userImageUri;
        }
        public PostObjectModel(PostObjectModel postObjectModel,ApplicationUser user,string profilePicture):base(user,profilePicture)
        {
            this.Post = postObjectModel.Post;
            this.ImageURI = postObjectModel.ImageURI;
            this.UserImageURI = postObjectModel.UserImageURI;
            this.Visits = postObjectModel.Visits;
        }

    }

    public class FullPostObjectModel : BasePageModel
    {
        public Post Post { get; set; }
        public string ImageURI { get; set; }
        public string UserImageURI { get; set; }
        public int Visits { get; set; }
        public int CommentsCount { get; set; }
        public CreatePostCommentModel CreatePostComment { get; set; }
        public List<PostCommentObjectModel> Comments { get; set; }

        public Tuple<bool,int> PostCertificates { get; set; }

        public FullPostObjectModel()
        {
            this.CreatePostComment = new CreatePostCommentModel();
        }
        public FullPostObjectModel(ApplicationUser user):base(user)
        {
            this.CreatePostComment = new CreatePostCommentModel();
        }
        public FullPostObjectModel(Post post, string imageUri, string userImageUri)
        {
            this.Post = post;
            this.ImageURI = imageUri;
            this.UserImageURI = userImageUri;
            this.CreatePostComment = new CreatePostCommentModel();
        }
        public FullPostObjectModel(FullPostObjectModel postObjectModel,ApplicationUser user, string profilePicture):base(user,profilePicture)
        {
            this.Post = postObjectModel.Post;
            this.ImageURI = postObjectModel.ImageURI;
            this.UserImageURI = postObjectModel.UserImageURI;
            this.Visits = postObjectModel.Visits;
            this.CreatePostComment = new CreatePostCommentModel();
            this.Comments = postObjectModel.Comments;
            this.CommentsCount = postObjectModel.Comments.Count;
        }
    }

    public class PostCommentObjectModel
    {
        public string Text { get; set; }
        public string Username { get; set; }
        public DateTime TimeStamp { get; set; }
        public string UserImage { get; set; }
    }

    public class CreatePostCommentModel
    {
        public string Text { get; set; }
        [Required]
        public string PostId { get; set; }
    }
}