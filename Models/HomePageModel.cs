using System.Collections.Generic;
using ViralLinks.Data;

namespace  ViralLinks.Models
{
    public class HomePageModel
    {
        public List<PostCategory> PostCategories { get; set; }
        public string SelectedCategory { get; set; }
        public PagedModel<List<Post>> Posts { get; set; }
    }
}