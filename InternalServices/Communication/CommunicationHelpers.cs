using System.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Serilog;
using ViralLinks.Data;

namespace ViralLinks.InternalServices
{
    public static class CommunicationHelpers
    {
        // public static async Task SendEmailVerification(this ICommunicationServices comms, ApplicationUser user, string link,string baseUrl)
        // {
        //     /// Get mail verification template
        //     // TODO :: REPLACE THIS ACCESS TO FILE SYSTEM STORING HTML TEMPLATES
        //     string templatePath =  "./wwwroot/emails/account_verification.html";
        //     // string logoPath = "./wwwroot/images/earn-edge-logo.png";
        //     // string logoWithText = "./wwwroot/images/earn-edge-text.png";

        //     string HtmlFormat = string.Empty;
        //     List<HtmlResource> resources = new List<HtmlResource>();
            
        //     // read html template to string 
        //     using(FileStream fs = new FileStream(templatePath, FileMode.Open))
        //     {
        //         using (StreamReader sr = new StreamReader(fs, Encoding.UTF8))
        //         {
        //             HtmlFormat = sr.ReadToEnd();
        //         }
        //     }

        //     // // Add picture to embedded resources and replace links to pictures in the message
        //     // var logoResource = new HtmlResource {
        //     //     ResourceID = Guid.NewGuid().ToString(),
        //     //     Data = System.IO.File.ReadAllBytes(logoPath),
        //     // };
        //     // HtmlFormat = HtmlFormat.Replace("$logo", string.Format("cid:{0}", logoResource.ResourceID));
        //     // resources.Add(logoResource);

        //     // // Add picture to embedded resources and replace links to pictures in the message
        //     // var logoWithTextResource = new HtmlResource {
        //     //     ResourceID = Guid.NewGuid().ToString(),
        //     //     Data = System.IO.File.ReadAllBytes(logoWithText),
        //     // };
        //     // HtmlFormat = HtmlFormat.Replace("$textlogo", string.Format("cid:{0}", logoWithTextResource.ResourceID));
        //     // resources.Add(logoWithTextResource);

        //      // add user's name, link and expiration time to html template
        //      HtmlFormat = HtmlFormat.Replace("$link",link)
        //          .Replace("$baseUrl", baseUrl);

        //     // add user firstname, code and expiration time to html template
        //     HtmlFormat = HtmlFormat.Replace("$baseUrl",baseUrl);
        //     // email to send 
        //     var emailModel = new Email{
        //         To = user.Email,
        //         ToName = $"{user.FirstName} {user.LastName}",
        //         From = "noreply@EarnEdge.com",
        //         FromName = "EarnEdge",
        //         IsHtml = true,
        //         Subject = "Account Verification",
        //         Body = HtmlFormat,
        //         LinkedResources = resources
        //     };

        //     // send email
        //     await comms.SendHtmlEmail(emailModel);
        //     Log.Information($"OTP email sent to {user.Email}");
        // }

        public static async Task SendWelcomeEmail(this ICommunicationServices comms, ApplicationUser user, string baseUrl)
        {
            // Get welcome mail template
            string templatePath =  "./wwwroot/emails/welcome.html";
            
            string HtmlFormat = string.Empty;
            List<HtmlResource> resources = new List<HtmlResource>();
            // read html template to string 
            using(FileStream fs = new FileStream(templatePath, FileMode.Open))
            {
                using (StreamReader sr = new StreamReader(fs, Encoding.UTF8))
                {
                    HtmlFormat = sr.ReadToEnd();
                }
            }

            // add user's name, link and expiration time to html template
            // HtmlFormat = HtmlFormat.Replace("$baseUrl", baseUrl);

            // email to send 
            var emailModel = new Email{
                To = user.Email,
                ToName = user.Email,
                From = "noreply@virallinks.com",
                FromName = "Viral Links",
                IsHtml = true,
                Subject = "Welcome to Viral Links",
                Body = HtmlFormat,
                LinkedResources = resources
            };

            // send email
            await comms.SendHtmlEmail(emailModel);
            Log.Information($"Welcome email sent to {user.Email}");
         }
        
        public static async Task SendPasswordReset(this ICommunicationServices comms, ApplicationUser user, string link, string baseUrl)
        {
            // Get mail verification template
            // TODO :: REPLACE THIS ACCESS TO FILE SYSTEM STORING HTML TEMPLATES
            string templatePath =  "./wwwroot/emails/password_reset.html";

            string HtmlFormat = string.Empty;
            List<HtmlResource> resources = new List<HtmlResource>();
            // read html template to string 
            using(FileStream fs = new FileStream(templatePath, FileMode.Open))
            {
                using (StreamReader sr = new StreamReader(fs, Encoding.UTF8))
                {
                    HtmlFormat = sr.ReadToEnd();
                }
            }

            // add user's name, link and expiration time to html template
            HtmlFormat = HtmlFormat.Replace("$link",link)
                .Replace("$baseUrl", baseUrl)
                .Replace("$email", user.Email);
            
            // email to send 
            var emailModel = new Email{
                To = user.Email,
                ToName = user.Email,
                From = "noreply@virallinks.com",
                FromName = "Viral Links",
                IsHtml = true,
                Subject = "Reset password",
                Body = HtmlFormat,
                LinkedResources = resources
            };

            // send email
            await comms.SendHtmlEmail(emailModel);
            Log.Information($"Password Reset email sent to {user.Email}");
        }


        public static string CapFirstLetter(this string word)
        {
            word = word.ToLower();
            var fl = word.First().ToString().ToUpper();
            word = word.Remove(0,1);
            return word = fl+word;
        }
    }
}