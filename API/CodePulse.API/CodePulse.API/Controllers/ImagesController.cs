using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
using CodePulse.API.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController(IImageRepository imageRepository) : ControllerBase
    {
        private readonly IImageRepository imageRepository = imageRepository;

        [HttpGet]
        public async Task<IActionResult> GetAllImages()
        {
            IEnumerable<BlogImage> blogImages = await imageRepository.GetBlogImages();

            List<BlogImageDTO> response = [];
            foreach (BlogImage blogImage in blogImages)
            {
                response.Add(new BlogImageDTO
                {
                    Id = blogImage.Id,
                    FileName = blogImage.FileName,
                    FileExtension = blogImage.FileExtension,
                    Title = blogImage.Title,
                    Url = blogImage.Url,
                    DateCreated = blogImage.DateCreated
                });
            }

            return Ok(response);
        }

        [HttpPost]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> UploadImage( IFormFile file, [FromForm] string fileName, [FromForm] string title)
        {
            ValidateFileUpload(file);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            BlogImage blogImage = new()
            {
                FileExtension=Path.GetExtension(file.FileName).ToLowerInvariant(),
                FileName = fileName,
                Title = title,
                DateCreated= DateTime.Now,
                Url = string.Empty
            };
            await imageRepository.Upload(file, blogImage);

            BlogImageDTO response = new()
            {
                Id = blogImage.Id,
                FileName = blogImage.FileName,
                FileExtension = blogImage.FileExtension,
                Title = blogImage.Title,
                Url = blogImage.Url,
                DateCreated = blogImage.DateCreated
            };
            return Ok(response);
        }

        private void ValidateFileUpload(IFormFile file)
        {
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var fileExtension = Path.GetExtension(file.FileName).ToLower();
            var maxFileSize = 10 * 1024 * 1024; // 10 MB
            if (!allowedExtensions.Contains(fileExtension))
            {
                ModelState.AddModelError("File", "Invalid file type. Only .jpg, .jpeg, .png, and .gif are allowed.");
            }
            if (file.Length > maxFileSize)
            {
                ModelState.AddModelError("File", "File size exceeds the maximum limit of 10 MB.");
            }
        }
    }
}
