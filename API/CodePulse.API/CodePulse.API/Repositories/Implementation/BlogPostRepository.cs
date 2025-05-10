using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Repositories.Implementation
{
    public class BlogPostRepository(ApplicationDbContext dbContext) : IBlogPostRepository
    {
        private readonly ApplicationDbContext dbContext = dbContext;

        public async Task<BlogPost> CreateAsync(BlogPost blogPost)
        {
            await dbContext.BlogPosts.AddAsync(blogPost);
            await dbContext.SaveChangesAsync();
            return blogPost;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var existing = await GetAsync(id);
            if (existing == null)
            {
                return false;
            }
            dbContext.BlogPosts.Remove(existing);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            var returnObj = await dbContext.BlogPosts.Include(x=>x.Categories).ToListAsync();
            return returnObj;
        }

        public async Task<BlogPost?> GetAsync(Guid id)
        {
            var returnObj = await dbContext.BlogPosts.Include(x=>x.Categories).FirstOrDefaultAsync(x=>x.Id==id);
            return returnObj;
        }

        public async Task<BlogPost?> GetByUrlAsync(string url)
        {
            var returnObj = await dbContext.BlogPosts.Include(x => x.Categories).FirstOrDefaultAsync(x => x.UrlHandle == url);
            return returnObj;
        }

        public async Task<BlogPost?> UpdateAsync(BlogPost blogPost)
        {
            var existing = await GetAsync(blogPost.Id);
            if (existing == null)
            {
                return null;
            }
            dbContext.Entry(existing).CurrentValues.SetValues(blogPost);
            existing.Categories = blogPost.Categories;
            await dbContext.SaveChangesAsync();
            return blogPost;
        }
    }
}
