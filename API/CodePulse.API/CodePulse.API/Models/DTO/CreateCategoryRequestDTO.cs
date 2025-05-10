namespace CodePulse.API.Models.DTO
{
    public class CreateCategoryRequestDTO
    {
        public required string Name { get; set; }
        public required string UrlHandle { get; set; }
    }
}
