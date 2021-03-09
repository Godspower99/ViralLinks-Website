using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;
using ViralLinks.Data;
using ViralLinks.InternalServices;
using ViralLinks.Models;
using ViralLinks.Helpers;
using System;

namespace ViralLinks.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly FileSystemService fileSystemService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ApplicationDbContext dbContext;
        private readonly IUrlGenerator urlGenerator;



        public HomeController(ILogger<HomeController> logger, ApplicationDbContext dbContext,
            IUrlGenerator urlGenerator, UserManager<ApplicationUser> userManager, FileSystemService fileSystemService)
        {
            _logger = logger ?? 
                throw new ArgumentNullException(nameof(logger));
            this.dbContext = dbContext ?? 
                throw new ArgumentNullException(nameof(dbContext));
            this.urlGenerator = urlGenerator ??
                throw new ArgumentNullException(nameof(urlGenerator));
            this.userManager = userManager ??
                throw new ArgumentNullException(nameof(userManager));
            this.fileSystemService = fileSystemService ??
                throw new ArgumentNullException(nameof(fileSystemService));
        }

        [HttpGet,Route(""),Route("home"),AllowAnonymous]
        public async Task<IActionResult> IndexGuest([FromQuery]PaginationFilter paginationFilter, string category = "all")
        {
            if(HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToAction(actionName:"IndexMember", controllerName: "Home");
            }

            var viewModel = new HomePageModel();
            viewModel.SelectedCategory = category;
            viewModel.PostCategories = await dbContext.GetPostCategories();

            var route = HttpContext.Request.Path;
            var validFilter = new PaginationFilter(pageNumber: paginationFilter.PageNumber, pageSize: paginationFilter.PageSize);

            var posts = await this.dbContext.GetPosts(category: category, amount: validFilter.PageSize);
            var postsCount = await this.dbContext.GetPostsCount(category);
            viewModel.Posts = PaginationHelpers<Post>.CreatePagedResponse(posts, validFilter,postsCount,urlGenerator,route);
            return View(model: viewModel);
        }

        [HttpGet,Route("home/member"), Authorize(AuthorizationPolicies.AuthenticatedPolicy)]
        public async Task<IActionResult> IndexMember([FromQuery]PaginationFilter paginationFilter, string category = "all")
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            if(user == null)
            {
                // redirect user to free homepage
                return RedirectToAction(actionName: "SignOut", controllerName: "Account");
            }
            var userPic = await fileSystemService.GetProfilePictureAsync(user.Id);
            var viewModel = new HomePageModel(user,userPic);
            viewModel.SelectedCategory = category;
            viewModel.PostCategories = await dbContext.GetPostCategories();

            var route = HttpContext.Request.Path;
            var validFilter = new PaginationFilter(pageNumber: paginationFilter.PageNumber, pageSize: paginationFilter.PageSize);
            var posts = await this.dbContext.GetPosts(category: category, amount: validFilter.PageSize);
            var postsCount = await this.dbContext.GetPostsCount(category);
            viewModel.Posts = PaginationHelpers<Post>.CreatePagedResponse(posts, validFilter,postsCount,urlGenerator,route);
            return View(model: viewModel);
        }

    }
}
