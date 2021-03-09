using ViralLinks.Data;

namespace ViralLinks
{
    public class BasePageModel
    {
        public ApplicationUser User { get; set; }
        public string ProfilePicture { get; set; }

        public BasePageModel(){}
        public BasePageModel(ApplicationUser user, string profilePicture)
        {
            this.User = user;
            this.ProfilePicture = profilePicture;
        }
        public BasePageModel(ApplicationUser user)
        {
            this.User = user;
        }
        public BasePageModel(string profilePicture)
        {
            this.ProfilePicture = profilePicture;
        }
    }
}