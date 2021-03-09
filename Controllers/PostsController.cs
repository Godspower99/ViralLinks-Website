using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ViralLinks.Data;
using ViralLinks.InternalServices;

namespace ViralLinks
{
    [Route("posts")]
    public class PostsController : Controller
    {
        private ApplicationDbContext context;
        private UserManager<ApplicationUser> userManager;
        private FileSystemService fileSystemService;
        public PostsController(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager,
            FileSystemService fileSystemService)
        {
            this.context = dbContext ??
                throw new ArgumentNullException(nameof(dbContext));
            this.userManager = userManager ??
                throw new ArgumentNullException(nameof(userManager));
            this.fileSystemService = fileSystemService ??
                throw new ArgumentNullException(nameof(fileSystemService));
        }

        [HttpGet, Route("{postid}/guest")]
        public async Task<ActionResult> FullPostGuest(string postid)
        {
            var post = await context.FindPost(postid);
            if(post == null)
            {
                // TODO :: Redirect to 404 page with returnUrl
                return RedirectToAction(actionName: "IndexGuest", controllerName: "Home");
            }
            return View(model: post);
        }


        [HttpGet, Route("create-post"), Authorize(AuthorizationPolicies.AuthenticatedPolicy)]
        public async Task<ActionResult> CreatePost()
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            if(user == null)
            {
                return RedirectToAction(actionName:"SignOut", controllerName:"Account");
            }
            var userpic = await fileSystemService.GetProfilePictureAsync(user.Id);
            var createPost = new CreatePostModel(user,userpic);
            return View(model: createPost);
        }

        [HttpPost,Route("create-post"),Authorize(AuthorizationPolicies.AuthenticatedPolicy),AutoValidateAntiforgeryToken]
        public async Task<ActionResult> CreatePost([Bind]CreatePostModel post)
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            if(user == null)
            {
                return RedirectToAction(actionName:"SignOut",controllerName:"Account");
            }
            var newPost = new Post();
            var cat = await context.FindPostCategory(post.CategoryId);
            newPost.CategoryHeader = cat.Header;
            newPost.CategoryId = cat.Id;
            newPost.Subject = post.Subject;
            newPost.Description = post.Description;
            newPost.
            await context.SavePost(newPost);
        }
    }
}