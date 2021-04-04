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
            var post = await context.FindFullPostObjectModel(postid,fileSystemService,userManager);
            if(post == null)
            {
                // TODO :: Redirect to 404 page with returnUrl
                return RedirectToAction(actionName: "IndexGuest", controllerName: "Home");
            }
            post.PostCertificates = await context.GetPostCertificate(postid,"guest");
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
            var post = await context.FindFullPostObjectModel(postid,fileSystemService,userManager);
            if(post == null)
            {
                // TODO :: Redirect to 404 page with returnUrl
                return RedirectToAction(actionName: "IndexMember", controllerName: "Home");
            }
            post = new FullPostObjectModel(post,user,userProfilePic);
            post.PostCertificates = await context.GetPostCertificate(postid,user.Id);
            return View(model: post);
        }

        [HttpGet, Route("create-post"), Authorize(AuthorizationPolicies.AuthenticatedPolicy)]
        public async Task<ActionResult> CreatePost(string selectedCategory)
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            if(user == null)
            {
                return RedirectToAction(actionName:"SignOut", controllerName:"Account");
            }

            // try find category
            var category = await context.FindPostCategory(selectedCategory);
            if(category == null || category.Id == "all")
            {
                return RedirectToAction(actionName:"IndexGuest",controllerName:"Home");
            }
            var userpic = fileSystemService.GetProfilePictureAsync(user.Id);
            var createPost = new CreatePostModel(user,userpic);
            createPost.CategoryId = selectedCategory;
            // var categories= await context.GetPostCategories();
            // categories.Remove(categories.FirstOrDefault(p => p.Id == "all"));
            ViewBag.Category = category;
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

        [HttpPost,Route("write-comment"),Authorize(AuthorizationPolicies.AuthenticatedPolicy),AutoValidateAntiforgeryToken]
        public async Task<ActionResult> WritePostComment(FullPostObjectModel commentModel)
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            if(user == null)
            {
                return RedirectToAction(actionName:"SignOut",controllerName:"Account");
            }

            // validate form model
            if(ModelState.IsValid)
            {
                var post = await context.FindPost(commentModel.CreatePostComment.PostId);
                // find post 
                if(post != null)
                {
                    await context.AddPostComment(new PostComment{
                        PostId = post.PostId,
                        UserId = user.Id,
                        Text = commentModel.CreatePostComment.Text
                    });
                    // redirect to full post page
                    return RedirectToAction(actionName:"FullPost",controllerName:"Posts",routeValues:new {postid = post.PostId});
                }
            }
            // TODO :: REDIRECT USER TO 404 PAGE
            return RedirectToAction(actionName: "IndexMember", controllerName:"Home");
        }

        [HttpPost,Route("certify-post"),Authorize(AuthorizationPolicies.AuthenticatedPolicy)]
        public async Task<ActionResult> CertifyPost(string postid)
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            if(user == null)
            {
                return RedirectToAction(actionName:"SignOut",controllerName:"Account");
            }
            var post = await context.FindPost(postid);
            if(post == null)
            {
                Console.WriteLine("NO SUCH POST  {0}", postid);
                return BadRequest();
            }
            var certInfo = await context.UpdatePostCertificate(postid,user.Id);
            return new JsonResult(new {
                count = certInfo.Item2,
                certified = certInfo.Item1
            });
        }

        [HttpGet,Route("certify-post"), Authorize(AuthorizationPolicies.AuthenticatedPolicy)]
        public async Task<ActionResult> GetPostCertification(string postid)
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            if(user == null)
            {
                return RedirectToAction(actionName:"SignOut",controllerName:"Account");
            }
            var post = await context.FindPost(postid);
            if(post == null)
            {
                return BadRequest();
            }
            var cert_info = await context.GetPostCertificate(postid, user.Id);
            return new JsonResult(new {
                count = cert_info.Item2,
                certified = cert_info.Item1,
            });
        }
    }
}