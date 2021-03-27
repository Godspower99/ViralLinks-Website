using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ViralLinks.Data;
using ViralLinks.InternalServices;
using ViralLinks.Models;

namespace ViralLinks
{
    [Route("user")]
    public class UserController : Controller
    {
        private UserManager<ApplicationUser> userManager;
        private FileSystemService fileSystemService;

        public UserController(UserManager<ApplicationUser> userManager, FileSystemService fileSystemService)
        {
            this.userManager = userManager ??
                throw new ArgumentNullException(nameof(userManager));
            this.fileSystemService = fileSystemService ??
                throw new ArgumentNullException(nameof(fileSystemService));
        }

        [HttpGet,Route("profile"),Authorize(AuthorizationPolicies.AuthenticatedPolicy)]
        public async Task<ActionResult> Profile()
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            if(user == null)
            {
                return RedirectToAction(actionName:"SignIn", controllerName: "Account");
            }
            var userPic = fileSystemService.GetProfilePictureAsync(user.Id);
            var profile = new ProfileModel(user,userPic);
            return View(model: profile);
        }
    }
}