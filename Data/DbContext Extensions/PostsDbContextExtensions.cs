using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ViralLinks.InternalServices;
using ViralLinks.Models;

namespace ViralLinks.Data
{
    public static class PostsDbContextExtensions
    {
        public static async Task<Post> FindPost(this ApplicationDbContext dbContext, string id)
        {
            return await dbContext.Posts.FirstOrDefaultAsync(p => p.PostId == id);
        }

        public static async Task<PostObjectModel> FindPostObjecModel(this ApplicationDbContext dbContext, string id, FileSystemService fileSystem)
        {
            var post = await dbContext.Posts.FirstOrDefaultAsync(p => p.PostId == id);
            if(post != null)
            {
                var userImageUri = fileSystem.GetProfilePictureAsync(post.UserId);
                var postImageUri = fileSystem.GetPostImageAsync(post.PostId);
                return new PostObjectModel(post,postImageUri,userImageUri)
                {
                    Visits = await dbContext.PostVisitCount(post.PostId),
                };
            }
            return null;
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

        public static async Task<List<PostObjectModel>> GetPostObjectModels(this ApplicationDbContext dbContext, FileSystemService fileSystem, string category = "all", int amount = 10)
        {
            var posts = await dbContext.GetPosts(category,amount);
            return posts.Select<Post,PostObjectModel>( p =>  {
                var userImageUri = fileSystem.GetProfilePictureAsync(p.UserId);
                var postImageUri = fileSystem.GetPostImageAsync(p.PostId);
                return new PostObjectModel(p,postImageUri,userImageUri)
                {
                    Visits = dbContext.PostVisitCount(p.PostId).GetAwaiter().GetResult()
                };
            }).ToList();
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

        public static async Task<List<PostObjectModel>> GetPostsByUserObjecModels(this ApplicationDbContext dbContext, FileSystemService fileSystem, string userid, int amount = 10)
        {
            var posts = await dbContext.GetPostsByUser(userid,amount);
            return posts.Select<Post,PostObjectModel>(p => {
                var userImageUri = fileSystem.GetProfilePictureAsync(p.UserId);
                var postImageUri = fileSystem.GetPostImageAsync(p.PostId);
                return new PostObjectModel(p,postImageUri,userImageUri);
            }).ToList();
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

        public static async Task RecordPostVisit(this ApplicationDbContext dbContext, PostLinkVisits visit)
        {
            await dbContext.PostLinkVisits.AddAsync(visit);
            await dbContext.SaveChangesAsync();
        }
        
        public static async Task<int> PostVisitCount(this ApplicationDbContext dbContext, string postid)
        {
            return await dbContext.PostLinkVisits.Where(v => v.PostId == postid).CountAsync();
        }

        public static async Task<List<PostObjectModel>> TopTrendingPosts(this ApplicationDbContext dbContext, FileSystemService fileSystem, int amount = 5)
        {
            var visits = await dbContext.PostLinkVisits.ToListAsync();
            var postvisits = visits.GroupBy(p => p.PostId).OrderByDescending(p => p.Count()).Take(amount);
            var postModels = new List<PostObjectModel>();
            foreach(var visit in postvisits)
            {
                var postObj = await dbContext.FindPostObjecModel(visit.Key, fileSystem);
                postObj.Visits = visit.Count();
                postModels.Add(postObj);
            }
            return postModels;
        }
    }
}