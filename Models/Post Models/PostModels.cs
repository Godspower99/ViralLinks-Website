using ViralLinks.Data;

namespace ViralLinks.Models
{
    public class PostObjectModel : BasePageModel
    {
        public Post Post { get; set; }
        public string ImageURI { get; set; }
        public string UserImageURI { get; set; }
        public int Visits { get; set; }

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
}