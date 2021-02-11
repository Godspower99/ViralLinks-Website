using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ViralLinks.Data
{
    public static class HomePageDbContextExtensions
    {
        public static async Task<List<PostCategory>> GetPostCategories(this ApplicationDbContext dbContext)
        {
            return await dbContext.PostCategories.OrderBy(p => p.Position).ToListAsync();
        }

        public static async Task<PostCategory> FindPostCategory(this ApplicationDbContext dbContext, string id)
        {
            return await dbContext.PostCategories.FirstOrDefaultAsync(p => p.Id == id);
        }

        public static async Task<List<PostCategory>> GetPostCategories(this ApplicationDbContext dbContext, string header)
        {
            var post_categories = dbContext.PostCategories.Where(p => p.Header.ToLower().Contains(header.ToLower()));
            return await post_categories.ToListAsync();
        }

    }
}