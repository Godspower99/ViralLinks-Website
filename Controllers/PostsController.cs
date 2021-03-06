using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ViralLinks.Data;

namespace ViralLinks
{
    [Route("posts")]
    public class PostsController : Controller
    {
        private ApplicationDbContext context;
        public PostsController(ApplicationDbContext dbContext)
        {
            context = dbContext;
        }

        [HttpGet, Route("{postid}")]
        public async Task<ActionResult> FullPost(string postid)
        {
            var post = await context.FindPost(postid);
            if(post == null)
            {
                // TODO :: Redirect to 404 page with returnUrl
                return RedirectToAction(actionName: "Index", controllerName: "Home");
            }
            return View(model: post);
        }
    }
}