using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace ViralLinks.Models
{
    public class SignUpFormPart1Model : BaseModel
    {
        public string ReferralID { get; set; }

        [Display(Name = "Username"),
        Required(ErrorMessage = "please enter a username"),
        Remote(action: "ValidateUsername", controller: "Account")]
        public string Username { get; set; }
        
        [Display(Name = "Email"),
        EmailAddress(ErrorMessage = "Invalid e-mail address"),
        Required(ErrorMessage = "Invalid e-mail address"),
        Remote(action: "ValidateEmail", controller: "Account")]     
        public string Email { get; set; }


        [Required]
        public string ID { get; set; }

        
        public SignUpFormPart1Model(){}

        public SignUpFormPart1Model(string referralId)
        {
            this.ReferralID = referralId;
            this.ID = Guid.NewGuid().ToString();
        }
    }

    public class SignUpFormPart2Model
    {
        [Display(Name = "Password"),
        DataType(DataType.Password),
        Required(ErrorMessage = "8 characters min"),
        MinLength(length: 8, ErrorMessage = "8 characters min")]
        public string Password { get; set; }

        [Display(Name = "Confirm Password"),
        DataType(DataType.Password),
        Required(ErrorMessage = "Confirm password"),
        Compare(nameof(Password), ErrorMessage = "Confirm password")]
        public string ConfirmPassword { get; set; }

        public string ReferralID { get; set; }

        [Required(ErrorMessage = "please enter a username"),
        Remote(action: "ValidateUsername", controller: "Account")]
        public string Username { get; set; }

        [EmailAddress(ErrorMessage = "Invalid e-mail address"),
        Required(ErrorMessage = "Invalid e-mail address"),
        Remote(action: "ValidateEmail", controller: "Account")]
        public string Email { get; set; }

        public SignUpFormPart2Model(){}

        public SignUpFormPart2Model(SignUpFormPart1Model form1)
        {
            this.ReferralID = form1.ReferralID;
            this.Username = form1.Username;
            this.Email = form1.Email;
        }
    }
}