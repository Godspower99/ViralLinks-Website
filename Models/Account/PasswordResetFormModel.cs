using System.ComponentModel.DataAnnotations;

namespace ViralLinks.Models
{
    public class PasswordResetFormModel
    {
        [Required]
        public string Token { get; set; }
        [Required]
        public string UserID { get; set; }
        
        [Display(Name = "Password"),
        DataType(DataType.Password),
        Required(ErrorMessage = "8 characters min"),
        MinLength(length: 8, ErrorMessage = "8 characters min")]
        public string NewPassword { get; set; }

        [Display(Name = "Confirm Password"),
        DataType(DataType.Password),
        Required(ErrorMessage = "Confirm password"),
        Compare(nameof(NewPassword), ErrorMessage = "Confirm password")]
        public string ConfirmPassword { get; set; }
    }
}