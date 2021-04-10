using System.Collections.Generic;
using ViralLinks.Data;

namespace ViralLinks.Models
{
    public class ProfileModel : BasePageModel
    {

        public PagedModel<List<PostObjectModel>> Posts { get; set; }
        
        public ProfileModel(){}
        public ProfileModel(ApplicationUser user):base(user){}
        public ProfileModel(string profilePicture):base(profilePicture){}
        public ProfileModel(ApplicationUser user, string profilePicture):base(user,profilePicture){}

    }
}