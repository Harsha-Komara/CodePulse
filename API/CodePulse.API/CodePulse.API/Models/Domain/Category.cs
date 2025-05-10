using CodePulse.API.Repositories.Interface;

namespace CodePulse.API.Models.Domain
{
    public class Category
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string UrlHandle { get; set; }

        public required ICollection<BlogPost> BlogPosts { get; set; }
    }
}
