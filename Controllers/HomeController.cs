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

namespace ViralLinks.Controllers
{
    [Route("home"), AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly FileSystemService fileSystemService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ApplicationDbContext dbContext;
        private readonly IUrlGenerator urlGenerator;



        public HomeController(ILogger<HomeController> logger, ApplicationDbContext dbContext,
            IUrlGenerator urlGenerator)
        {
            _logger = logger;
            this.dbContext = dbContext;
            this.urlGenerator = urlGenerator;
        }

        [HttpGet,Route("")]
        public async Task<IActionResult> Index([FromQuery]PaginationFilter paginationFilter, string category = "all")
        {
            var viewModel = new HomePageModel();
            viewModel.SelectedCategory = category;
            viewModel.PostCategories = await dbContext.GetPostCategories();

            var route = HttpContext.Request.Path;
            var validFilter = new PaginationFilter(pageNumber: paginationFilter.PageNumber, pageSize: paginationFilter.PageSize);
            var posts = await this.dbContext.GetPostObjectModels(userManager, category: category, amount: validFilter.PageNumber);
            var postsCount = await this.dbContext.GetPostsCount(category);

            viewModel.Posts = PaginationHelpers<PostObjectModel>.CreatePagedResponse(posts, validFilter,postsCount,urlGenerator,route);
            return View(model: viewModel);
        }

        [HttpGet, Route("membership-description")]
        public ActionResult MembershipDescription()
        {
            return View();
        }

    }
}
