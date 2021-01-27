using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ViralLinks.Models
{
    public class LoginFormModel : BaseModel
    {
        [DisplayName("Email"),Required]
        public string UsernameOrEmail { get; set; }
        
        [DisplayName("Password"),Required]
        public string Password { get; set; }
    }
}