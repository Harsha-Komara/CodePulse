using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Repositories.Implementation
{
    public class ImageRepository(IWebHostEnvironment webHostEnvironment,IHttpContextAccessor httpContextAccessor,ApplicationDbContext dbContext) : IImageRepository
    {
        private readonly IWebHostEnvironment webHostEnvironment = webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor = httpContextAccessor;
        private readonly ApplicationDbContext dbContext = dbContext;

        public async Task<IEnumerable<BlogImage>> GetBlogImages()
        {
            return await dbContext.BlogImages.ToListAsync();
        }

        public async Task<BlogImage> Upload(IFormFile file, BlogImage blogImage)
        {
            if(httpContextAccessor.HttpContext is null)
            {
                throw new Exception("HttpContext is null");
            }
            var fileName = blogImage.FileName + blogImage.FileExtension;
            var localPath = Path.Combine(webHostEnvironment.ContentRootPath, "Images",fileName);
            using (var stream = new FileStream(localPath, FileMode.Create))
            {
               await file.CopyToAsync(stream);
            }

            var httpRequest = httpContextAccessor.HttpContext.Request;
            var baseUrl = $"{httpRequest.Scheme}://{httpRequest.Host}{httpRequest.PathBase}";
            var imageUrl = $"{baseUrl}/Images/{fileName}";
            blogImage.Url = imageUrl;
            await dbContext.BlogImages.AddAsync(blogImage);
            await dbContext.SaveChangesAsync();

            return blogImage;
        }
    }
}
