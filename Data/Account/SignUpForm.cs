using System.ComponentModel.DataAnnotations;
using ViralLinks.Models;

namespace ViralLinks.Data
{
    public class SignUpForm
    {
        [Key]
        public string ID { get; set; }
        public string ReferralID { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }

        public SignUpForm(){}

        public SignUpForm(SignUpFormPart1Model formModel)
        {
            this.ID = formModel.ID;
            this.ReferralID = formModel.ReferralID;
            this.Username = formModel.Username;
            this.Email = formModel.Email;
        }
    }
}