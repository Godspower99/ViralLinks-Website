using System.ComponentModel.DataAnnotations;

namespace ViralLinks.Models
{
    public class ForgotPasswordFormModel
    {
        [Display(Name = "Email"),
        EmailAddress(ErrorMessage = "Invalid e-mail address"),
        Required(ErrorMessage = "Invalid e-mail address"),]
        public string Email { get; set; }
    }
}