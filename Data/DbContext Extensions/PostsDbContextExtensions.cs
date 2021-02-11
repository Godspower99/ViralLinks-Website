using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ViralLinks.Models;

namespace ViralLinks.Data
{
    public static class PostsDbContextExtensions
    {
        public static async Task<Post> FindPost(this ApplicationDbContext dbContext, string id)
        {
            return await dbContext.Posts.FirstOrDefaultAsync(p => p.Id == id);
        }

        public static async Task<List<Post>> GetPosts(this ApplicationDbContext dbContext, string category ="all", int amount = 10)
        {
            if(category == "all")
                return await dbContext.Posts.OrderByDescending(p => p.TimeStamp).Take(amount).ToListAsync();
            var postCategory = await dbContext.PostCategories.FirstOrDefaultAsync(pc => pc.Id == category);
            if(category == null)
                return await dbContext.Posts.OrderByDescending(p => p.TimeStamp).Take(amount).ToListAsync();
            return await dbContext.Posts.Where(p => p.CategoryId == category).OrderByDescending(p => p.TimeStamp).Take(amount).ToListAsync();
        }

        public static async Task<int> GetPostsCount(this ApplicationDbContext dbContext, string category ="all")
        {
            if(category == "all")
                return await dbContext.Posts.CountAsync();
            var postCategory = await dbContext.PostCategories.FirstOrDefaultAsync(pc => pc.Id == category);
            if(category == null)
                return await dbContext.Posts.CountAsync();
            return await dbContext.Posts.Where(p => p.CategoryId == category).CountAsync();
        }

        public static async Task<List<Post>> GetPostsByUser(this ApplicationDbContext dbContext, string userId, int amount = 10)
        {
            var userPosts =  dbContext.Posts.Where(p => p.UserId == userId).OrderByDescending(p => p.TimeStamp);
            return await userPosts.Take(amount).ToListAsync();
        }

        public static async Task SavePost(this ApplicationDbContext dbContext, Post post)
        {
            await dbContext.Posts.AddAsync(post);
            await dbContext.SaveChangesAsync();
        }

        public static async Task UpdatePost(this ApplicationDbContext dbContext, Post post)
        {
            dbContext.Posts.Update(post);
            await dbContext.SaveChangesAsync();
        }

        public static async Task DeletePost(this ApplicationDbContext dbContext, Post post)
        {
            dbContext.Posts.Remove(post);
            await dbContext.SaveChangesAsync();
        }

        public static async Task<List<PostObjectModel>> GetPostObjectModels(this ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager, string category = "all", int amount = 10)
        {
            var posts = new List<Post>();
            var postCategory = await dbContext.PostCategories.FirstOrDefaultAsync(pc => pc.Id == category);
             if(postCategory == null || category == "all")
                posts =await dbContext.Posts.OrderByDescending(p => p.TimeStamp).Take(amount).ToListAsync();
            else
                posts = await dbContext.Posts.Where(p => p.CategoryId == category).OrderByDescending(p => p.TimeStamp).Take(amount).ToListAsync();
            
            var postObjectModels = new List<PostObjectModel>();

            posts.ForEach(async p =>  {
                postObjectModels.Add(new PostObjectModel{
                    Post = p,
                    User = await userManager.Users.FirstOrDefaultAsync(u => u.Id == p.UserId)
                });
            });
            return postObjectModels;
        }
    }
}