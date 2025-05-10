using Azure;
using Azure.Core;
using CodePulse.API.Data;
using CodePulse.API.Migrations;
using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
using CodePulse.API.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController(ICategoryRepository categoryRepository) : ControllerBase
    {
        private readonly ICategoryRepository categoryRepository = categoryRepository;

        [HttpPost]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryRequestDTO request)
        {
            Category category = new()
            {
                Name = request.Name,
                UrlHandle = request.UrlHandle,
                BlogPosts = []
            };

            category = await categoryRepository.CreateAsync(category);

            CategoryDTO response = new()
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle
            };

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories([FromQuery] string? search,
            [FromQuery] string? sortBy, [FromQuery] string? sortOrder)
        {
            IEnumerable<Category> categories = await categoryRepository.GetAllAsync(search, sortBy, sortOrder);

            var response = categories.Select(c => new CategoryDTO
            {
                Id = c.Id,
                Name = c.Name,
                UrlHandle = c.UrlHandle
            });

            return Ok(response);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Reader,Writer")]
        public async Task<IActionResult> GetCategory([FromRoute] Guid id)
        {
            var category = await categoryRepository.GetAsync(id);
            if (category == null)
            {
                return NotFound("Category not found");
            }
            CategoryDTO response = new()
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle
            };
            return Ok(response);
        }

        [HttpPut("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> UpdateCategory([FromRoute] Guid id,[FromBody] UpdateCategoryRequestDTO request)
        {
            Category? category = new()
            {
                Id = id,
                Name = request.Name,
                UrlHandle = request.UrlHandle,
                BlogPosts = []
            };

            category = await categoryRepository.UpdateAsync(category);
            if (category == null)
            {
                return NotFound("Category not found");
            }
            CategoryDTO response = new()
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle
            };

            return Ok(response);
        }

        [HttpDelete("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> DeleteCategory([FromRoute] Guid id)
        {
            if(await categoryRepository.DeleteAsync(id))
            {
                return Ok();
            }
            else
            {
                return NotFound("Category not found");
            }
        }
    }
}
