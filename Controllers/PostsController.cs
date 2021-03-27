using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ViralLinks.Data;
using ViralLinks.InternalServices;
using ViralLinks.Models;

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

        [HttpGet, Route("guest")]
        public async Task<ActionResult> FullPostGuest(string postid)
        {
            var post = await context.FindPostObjecModel(postid,fileSystemService);
            if(post == null)
            {
                // TODO :: Redirect to 404 page with returnUrl
                return RedirectToAction(actionName: "IndexGuest", controllerName: "Home");
            }
            return View(model: post);
        }

        [HttpGet, Route("member")]
        public async Task<ActionResult> FullPost(string postid)
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            if(user == null)
            {
                return RedirectToAction(actionName: "SignOut", controllerName: "Account");
            }
            var userProfilePic = fileSystemService.GetProfilePictureAsync(user.Id);
            var post = await context.FindPostObjecModel(postid,fileSystemService);
            post = new PostObjectModel(post,user,userProfilePic);
            if(post == null)
            {
                // TODO :: Redirect to 404 page with returnUrl
                return RedirectToAction(actionName: "IndexMember", controllerName: "Home");
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
            var userpic = fileSystemService.GetProfilePictureAsync(user.Id);
            var createPost = new CreatePostModel(user,userpic);
            var categories= await context.GetPostCategories();
            categories.Remove(categories.FirstOrDefault(p => p.Id == "all"));
            ViewBag.Categories = categories;
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
            // process only valid models
            if(ModelState.IsValid)
            {
                var postCategory = await context.FindPostCategory(post.CategoryId);
                // continue if post category is found
                if(postCategory != null)
                {
                    var userImage = fileSystemService.GetProfilePictureAsync(user.Id);
                    var newPost = new Post(user,post,postCategory);
                    await context.SavePost(newPost);
                    // upload post image
                    await fileSystemService.UploadPostImage(post,newPost);
                    // redirect to full post page
                    return RedirectToAction(actionName: "FullPost",controllerName:"Posts",routeValues: new {
                        postid = newPost.PostId,
                    });
                }
            }
            return BadRequest();
        }

        [HttpGet("goto")]
        public async Task<ActionResult> GotoPostLink(string postId)
        {
            // try find post
            var post = await context.FindPost(postId);
            if(post == null)
            {
                // TODO :: REDIRECT TO 404 PAGE
                return RedirectToAction(actionName:"IndexGuest", controllerName: "Home");
            }
            var userId = "guest";
            if(HttpContext.User.Identity.IsAuthenticated)
            {
                var user = await userManager.GetUserAsync(HttpContext.User);
                if(user != null)
                    userId = user.Id;
            }
            // record new post visit
            await context.RecordPostVisit(new PostLinkVisits(userId,postId));
            // redirect user to post link destination
            Response.Redirect(post.PostLink);
            return Redirect(post.PostLink);
        }
    }
}