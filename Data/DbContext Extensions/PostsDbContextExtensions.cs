using System;
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

        public static async Task<PostObjectModel> FindPostObjecModel(this ApplicationDbContext dbContext, string id, FileSystemService fileSystem, string userid = "guest")
        {
            var post = await dbContext.Posts.FirstOrDefaultAsync(p => p.PostId == id);
            if(post != null)
            {
                var userImageUri = fileSystem.GetProfilePictureAsync(post.UserId);
                var postImageUri = fileSystem.GetPostImageAsync(post.PostId);
                return new PostObjectModel(post,postImageUri,userImageUri)
                {
                    CommentsCount = await dbContext.PostCommentCount(post.PostId),
                    Visits = await dbContext.PostVisitCount(post.PostId),
                    PostSaved = await dbContext.GetSavedPostStatus(id,userid)
                };
            }
            return null;
        }

        public static async Task<FullPostObjectModel> FindFullPostObjectModel(this ApplicationDbContext dbContext, string id, FileSystemService fileSystem,UserManager<ApplicationUser> userManager, string userid = "guest")
        {
            var post = await dbContext.Posts.FirstOrDefaultAsync(p => p.PostId == id);
            if(post != null)
            {
                var userImageUri = fileSystem.GetProfilePictureAsync(post.UserId);
                var postImageUri = fileSystem.GetPostImageAsync(post.PostId);
                var comments = await dbContext.GetPostCommentObjectModels(userManager,fileSystem,id);

                return new FullPostObjectModel(post,postImageUri,userImageUri)
                {
                    Visits = await dbContext.PostVisitCount(post.PostId),
                    Comments = comments,
                    CommentsCount = comments.Count,
                    PostSaved = await dbContext.GetSavedPostStatus(id,userid),
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

        public static async Task<List<PostObjectModel>> GetPostObjectModels(this ApplicationDbContext dbContext, FileSystemService fileSystem, string userid="guest", string category = "all", int amount = 10)
        {
            var posts = await dbContext.GetPosts(category,amount);
            return posts.Select<Post,PostObjectModel>( p =>  {
                var userImageUri = fileSystem.GetProfilePictureAsync(p.UserId);
                var postImageUri = fileSystem.GetPostImageAsync(p.PostId);
                return new PostObjectModel(p,postImageUri,userImageUri)
                {
                    Visits = dbContext.PostVisitCount(p.PostId).GetAwaiter().GetResult(),
                    CommentsCount = dbContext.PostCommentCount(p.PostId).GetAwaiter().GetResult(),
                    PostCertificates = dbContext.GetPostCertificate(p.PostId,userid).GetAwaiter().GetResult(),
                    PostSaved = dbContext.GetSavedPostStatus(p.PostId,userid).GetAwaiter().GetResult(),
                };
            }).ToList();
        }
        public static async Task<List<PostObjectModel>> GetPostsByUserObjecModels(this ApplicationDbContext dbContext, FileSystemService fileSystem, string userid, int amount = 10)
        {
            var posts = await dbContext.GetPostsByUser(userid,amount);
            return posts.Select<Post,PostObjectModel>(p => {
                var userImageUri = fileSystem.GetProfilePictureAsync(p.UserId);
                var postImageUri = fileSystem.GetPostImageAsync(p.PostId);
                return new PostObjectModel(p,postImageUri,userImageUri)
                {
                    Visits = dbContext.PostVisitCount(p.PostId).GetAwaiter().GetResult(),
                    CommentsCount = dbContext.PostCommentCount(p.PostId).GetAwaiter().GetResult(),
                    PostCertificates = dbContext.GetPostCertificate(p.PostId,userid).GetAwaiter().GetResult(),
                    PostSaved = dbContext.GetSavedPostStatus(p.PostId,userid).GetAwaiter().GetResult(),
                };
            }).ToList();
        }

        public static async Task<List<PostObjectModel>> GetSavedPostsObjectModels(this ApplicationDbContext dbContext, FileSystemService fileSystem, string userid = "guest", int amount = 10)
        {
            var savedPosts = await dbContext.GetSavedPosts(userid);
            var posts = new List<Post>();
            foreach(var s in savedPosts)
                posts.Add(await dbContext.FindPost(s));
            return posts.Select<Post,PostObjectModel>(p => {
                var userImageUri = fileSystem.GetProfilePictureAsync(p?.UserId);
                var postImageUri = fileSystem.GetPostImageAsync(p?.PostId);
                return new PostObjectModel(p,postImageUri,userImageUri)
                {
                    Visits = dbContext.PostVisitCount(p?.PostId).GetAwaiter().GetResult(),
                    CommentsCount = dbContext.PostCommentCount(p?.PostId).GetAwaiter().GetResult(),
                    PostCertificates = dbContext.GetPostCertificate(p?.PostId,userid).GetAwaiter().GetResult(),
                    PostSaved = dbContext.GetSavedPostStatus(p.PostId,userid).GetAwaiter().GetResult(),
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

        public static async Task<int> GetUserPostsCount(this ApplicationDbContext dbContext, string userid)
        {
            return await dbContext.Posts.Where(p => p.UserId == userid).CountAsync();
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

        public static async Task<List<PostComment>> GetPostComments(this ApplicationDbContext dbContext, string postId)
        {
            return await dbContext.PostComments.Where(pc => pc.PostId == postId).OrderByDescending(pc => pc.TimeStamp).ToListAsync();
        }

        public static async Task AddPostComment(this ApplicationDbContext dbContext, PostComment postComment)
        {
            await dbContext.PostComments.AddAsync(postComment);
            await dbContext.SaveChangesAsync();
        }

        public static async Task<int> PostCommentCount(this ApplicationDbContext dbContext, string postId)
        {
            return await dbContext.PostComments.Where(pc => pc.PostId == postId).CountAsync();
        }

        public static async Task<List<PostCommentObjectModel>> GetPostCommentObjectModels(this ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager, FileSystemService fileSystem, string postid)
        {
            var postComments = await dbContext.GetPostComments(postid);
            var comments = new List<PostCommentObjectModel>();
            foreach(var pc in postComments)
            {
                comments.Add(new PostCommentObjectModel{
                    Text = pc.Text,
                    TimeStamp = pc.TimeStamp,
                    UserImage = fileSystem.GetProfilePictureAsync(pc.UserId),
                    Username = (await userManager.FindByIdAsync(pc.UserId)).UserName,
                });
            }
            return comments;
        }

        public static async Task<int> GetPostCertificateCount(this ApplicationDbContext dbContext, string postid)
        {
            return (await dbContext.PostCertificates.Where(pc => pc.PostId == postid).ToListAsync()).GroupBy(p => p.UserId).Count();
        }

        public static async Task<Tuple<bool,int>> UpdatePostCertificate(this ApplicationDbContext dbContext, string postid, string userid)
        {
            // add new certificate if none is found
            var cert = await dbContext.PostCertificates.Where(pc => pc.PostId == postid && pc.UserId == userid).ToListAsync();
            if(cert.Count == 0)
            {
                await dbContext.PostCertificates.AddAsync(new PostCertificate{
                    PostId = postid,
                    UserId = userid,
                    TimeStamp = DateTime.Now,
                });
                await dbContext.SaveChangesAsync();
                var count = await dbContext.GetPostCertificateCount(postid);    
                return Tuple.Create<bool,int>(item1: true, item2: count);
            }
            dbContext.PostCertificates.RemoveRange(cert);            
            await dbContext.SaveChangesAsync();
            var x = await dbContext.GetPostCertificateCount(postid);
            return Tuple.Create<bool,int>(item1: false, item2: x);
        }

        public static async Task<Tuple<bool,int>> GetPostCertificate(this ApplicationDbContext dbContext, string postid, string userid)
        {
            var count = await dbContext.GetPostCertificateCount(postid);
            var certified = userid == "guest" ? false : await dbContext.PostCertificates.AnyAsync(pc => pc.PostId == postid && pc.UserId == userid);
            return Tuple.Create<bool,int>(item1: certified, item2: count);
        }

        public static async Task<int> GetSavedPostsCount(this ApplicationDbContext dbContext, string userid)
        {
            return await dbContext.SavedPosts.Where(sp => sp.UserId == userid).CountAsync();
        }
        public static async Task<bool> UpdateSavedPost(this ApplicationDbContext dbContext, string postid, string userid)
        {
            // add new saved post if none is found
            var savedPost = await dbContext.SavedPosts.Where(s => s.PostId == postid && s.UserId == userid).ToListAsync();
            if(savedPost.Count == 0)
            {
                await dbContext.SavedPosts.AddAsync(new SavedPost(userid,postid));
                await dbContext.SaveChangesAsync();
                return true;
            }
            dbContext.SavedPosts.RemoveRange(savedPost);
            await dbContext.SaveChangesAsync();
            return false;
        }

        public static async Task<bool> GetSavedPostStatus(this ApplicationDbContext dbContext, string postid, string userid)
        {
            return await dbContext.SavedPosts.AnyAsync(p => p.PostId == postid && p.UserId == userid);
        }

        public static async Task<List<string>> GetSavedPosts(this ApplicationDbContext dbContext, string userid)
        {
            return await dbContext.SavedPosts.Where(p => p.UserId == userid).Select(p => p.PostId).ToListAsync();
        }
    }
}