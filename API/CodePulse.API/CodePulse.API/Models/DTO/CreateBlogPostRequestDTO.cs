namespace CodePulse.API.Models.DTO
{
    public class CreateBlogPostRequestDTO
    {
        public required string Title { get; set; }
        public string? ShortDescription { get; set; }
        public string? Content { get; set; }
        public string? FeaturedImageUrl { get; set; }
        public required string UrlHandle { get; set; }
        public DateTime PublishedDate { get; set; }
        public required string Author { get; set; }
        public bool IsVisible { get; set; }

        public required Guid[] Categories { get; set; }
    }
}
