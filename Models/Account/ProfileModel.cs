using System.Net.Http;
using System.Net;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using ViralLinks.Data;
using ViralLinks.ValidationAttributes;
using static System.Net.WebRequestMethods;
using System.IO;
using System;

namespace ViralLinks.Models
{
    public class ProfileModel : BasePageModel
    {
        [MaxFileSize(5), AllowedExtensions(".jpg", ".png", ".jpeg")]
        public IFormFile ProfilePictureUpdate { get; set; }

        public PagedModel<List<PostObjectModel>> Posts { get; set; }
        
        public ProfileModel(){}
        public ProfileModel(ApplicationUser user):base(user){}
        public ProfileModel(string profilePicture):base(profilePicture){}
        public ProfileModel(ApplicationUser user, string profilePicture):base(user,profilePicture){}

    }

    public class ChangePasswordModel : BasePageModel
    {
        public ChangePasswordModel(){}
        public ChangePasswordModel(ApplicationUser user):base(user){}
        public ChangePasswordModel(string profilePicture):base(profilePicture){}
        public ChangePasswordModel(ApplicationUser user, string profilePicture):base(user,profilePicture){}


        // Form Data

        [Display(Name = "Password"),
        DataType(DataType.Password),
        Required(ErrorMessage = "8 characters min"),
        MinLength(length: 8, ErrorMessage = "8 characters min")]
        public string CurrentPassword { get; set; }

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

    public class UpdateProfilePicture
    {

        public ByteArrayContent FileBlob { get; set; }
        public string FilePath { get; set; }
     
    }
}