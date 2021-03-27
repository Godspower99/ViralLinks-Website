using System.Collections.Generic;
using ViralLinks.Data;

namespace  ViralLinks.Models
{
    public class HomePageModel : BasePageModel
    {
        public List<PostCategory> PostCategories { get; set; }
        public string SelectedCategory { get; set; }
        public PagedModel<List<PostObjectModel>> Posts { get; set; }
        public List<PostObjectModel> TopTrendingPosts { get; set; }

        public HomePageModel(){}

        public HomePageModel(ApplicationUser user, string profilePicture):base(user,profilePicture){}
        public HomePageModel(ApplicationUser user):base(user){}
        public HomePageModel(string profilePicture):base(profilePicture){}
    }
}