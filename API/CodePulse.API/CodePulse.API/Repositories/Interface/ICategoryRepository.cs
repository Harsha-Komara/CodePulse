using CodePulse.API.Models.Domain;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.API.Repositories.Interface
{
    public interface ICategoryRepository
    {
        Task<Category> CreateAsync(Category category);
        Task<IEnumerable<Category>> GetAllAsync(string? search, string? sortBy, string? sortOrder);
        Task<Category?> GetAsync(Guid id);
        Task<Category?> UpdateAsync(Category category);
        Task<bool> DeleteAsync(Guid id);
    }
}
