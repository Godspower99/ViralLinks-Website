using System.Net.Mime;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ViralLinks.Data;
using ViralLinks.InternalServices;
using ViralLinks.Models;
using ViralLinks.Helpers;

namespace ViralLinks
{
    [Route("user")]
    public class UserController : Controller
    {
        private UserManager<ApplicationUser> userManager;
        private FileSystemService fileSystemService;
        private ApplicationDbContext dbContext;
        private IUrlGenerator urlGenerator;

        public UserController(UserManager<ApplicationUser> userManager, FileSystemService fileSystemService,
            ApplicationDbContext context, IUrlGenerator urlGenerator)
        {
            this.userManager = userManager ??
                throw new ArgumentNullException(nameof(userManager));
            this.fileSystemService = fileSystemService ??
                throw new ArgumentNullException(nameof(fileSystemService));
            this.dbContext = context ?? 
                throw new ArgumentNullException(nameof(context));
            this.urlGenerator = urlGenerator ?? 
                throw new ArgumentNullException(nameof(urlGenerator));
        }

        [HttpGet,Route("profile"),Authorize(AuthorizationPolicies.AuthenticatedPolicy)]
        public async Task<ActionResult> Profile([FromQuery]PaginationFilter paginationFilter)
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            if(user == null)
            {
                return RedirectToAction(actionName:"SignIn", controllerName: "Account");
            }
            var userPic = fileSystemService.GetProfilePictureAsync(user.Id);
            var viewModel = new ProfileModel(user,userPic);

            var route = HttpContext.Request.Path;
            var validFilter = new PaginationFilter(pageNumber: paginationFilter.PageNumber, pageSize: paginationFilter.PageSize);
            var posts = await this.dbContext.GetPostsByUserObjecModels(fileSystemService,user.Id,validFilter.PageSize);
            var postsCount = await this.dbContext.GetUserPostsCount(user.Id);
            viewModel.Posts = PaginationHelpers<PostObjectModel>.CreatePagedResponse(posts, validFilter,postsCount,urlGenerator,route);
            return View(model: viewModel);
        }
    }
}