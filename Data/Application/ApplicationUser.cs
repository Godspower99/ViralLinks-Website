using System;
using Microsoft.AspNetCore.Identity;

namespace ViralLinks.Data
{
    public class ApplicationUser : IdentityUser
    {
        public DateTime LastLoginTimeStamp { get; set; }
        public string LastLoginIpAddress { get; set; }
        public string Bank { get; set; }
        public string AccountNumber { get; set; }
        public string AccountName { get; set; }
    }

}