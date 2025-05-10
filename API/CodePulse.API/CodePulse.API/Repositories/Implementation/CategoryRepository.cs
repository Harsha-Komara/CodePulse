using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Repositories.Interface;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace CodePulse.API.Repositories.Implementation
{
    public class CategoryRepository(ApplicationDbContext dbContext) : ICategoryRepository
    {
        public async Task<Category> CreateAsync(Category category)
        {
            await dbContext.Categories.AddAsync(category);
            await dbContext.SaveChangesAsync();
            return category;
        }

        public async Task<IEnumerable<Category>> GetAllAsync(string? search, string? sortBy, string? sortOrder)
        {
            
            var categories = dbContext.Categories.AsQueryable();

            if(!string.IsNullOrWhiteSpace(search))
            {
                categories = categories.Where(x => x.Name.Contains(search.Trim()));
            }

            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                var isDescending = sortOrder == "desc";

                categories = sortBy.Trim().ToLower() switch
                {
                    "name" => isDescending ? categories.OrderByDescending(x => x.Name) : categories.OrderBy(x => x.Name),
                    "urlhandle" => isDescending ? categories.OrderByDescending(x => x.UrlHandle) : categories.OrderBy(x => x.UrlHandle),
                    _ => isDescending? categories.OrderByDescending(x => x.Id) : categories.OrderBy(x => x.Id),
                };
            }

            return await categories.ToListAsync(); ;
        }

        public async Task<Category?> GetAsync(Guid id)
        {
            var returnObj = await dbContext.Categories.FirstOrDefaultAsync(x=>x.Id==id);
            return returnObj;
        }

        public async Task<Category?> UpdateAsync(Category category)
        {
            var existing = await GetAsync(category.Id);
            if (existing == null)
            {
                return null;
            }
            dbContext.Entry(existing).CurrentValues.SetValues(category);
            await dbContext.SaveChangesAsync();
            return category;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var existing = await GetAsync(id);
            if (existing == null)
            {
                return false;
            }
            dbContext.Categories.Remove(existing);
            await dbContext.SaveChangesAsync();
            return true;
        }
    }
}
