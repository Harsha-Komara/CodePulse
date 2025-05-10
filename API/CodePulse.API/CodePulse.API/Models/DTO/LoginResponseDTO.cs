namespace CodePulse.API.Models.DTO
{
    public class LoginResponseDTO
    {
        public required string UserName { get; set; }
        public required string Token { get; set; }
        public required List<string> Roles { get; set; }
    }
}
