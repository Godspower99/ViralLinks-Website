using ViralLinks.Data;

namespace ViralLinks.Models
{
    public class PostObjectModel
    {
        public ApplicationUser User { get; set; }
        public Post Post { get; set; }
    }
}