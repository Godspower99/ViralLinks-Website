using System.Net.Mime;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ViralLinks.Data;

namespace ViralLinks
{
    [ApiController,Route("test")]
    public class TestController : ControllerBase
    {
        ApplicationDbContext context;
        public TestController(ApplicationDbContext dbContext)
        {
            context = dbContext;
        }

        [HttpPost, Route("create-post")]
        public async Task<IActionResult> AddPost(CreatePostTest post)
        {
            var createPost = new CreatePostTest(post);
            await context.SavePost(new Post{
                UserId = createPost.UserId,
                PostId = createPost.PostId,
                CategoryId = createPost.CategoryId,
                Username = createPost.Username,
                Subject = createPost.Subject,
                Description = createPost.Description,
                PostLink = createPost.PostLink,
                TimeStamp = createPost.TimeStamp,
                ImageURI = createPost.ImageURI,
                UserImageURI = createPost.UserImageURI,
                CategoryHeader = createPost.CategotyHeader
            });
            return Ok();
        }

        [HttpGet,Route("flush-posts")]
        public async Task<IActionResult> FlushPosts()
        {
            var posts = await context.GetPosts(amount: 100);
            foreach(var post in posts)
                await context.DeletePost(post);
            return Ok();
        }

    }

    public class CreatePostTest
    {
        public string PostId { get; set; }
        public string CategoryId { get; set; }
        public string CategotyHeader { get; set; }
        public string UserId { get; set; }
        public string Username { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public string PostLink { get; set; }
        public DateTime TimeStamp { get; set; }
        public string ImageURI { get; set; }
        public string UserImageURI {get; set;}

        public CreatePostTest()
        {
        }

        public CreatePostTest(CreatePostTest post)
        {
            this.PostId = Guid.NewGuid().ToString(); this.CategoryId = post.CategoryId;this.UserId = post.UserId;
            this.Username = post.Username; this.Subject = post.Subject; this.Description = post.Description;
            this.TimeStamp = DateTime.Now; this.ImageURI = post.ImageURI; this.UserImageURI = post.UserImageURI;
            this.CategotyHeader = post.CategotyHeader; this.PostLink = post.PostLink;
        }
    }
}