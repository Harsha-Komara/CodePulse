using CodePulse.API.Models.DTO;
using CodePulse.API.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CodePulse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository) : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager = userManager;
        private readonly ITokenRepository tokenRepository = tokenRepository;

        [HttpPost("registeer")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO request)
        {
            var user = new IdentityUser
            {
                UserName = request.UserName.Trim(),
                Email = request.Email.Trim()
            };
            
            IdentityResult identityResult = await userManager.CreateAsync(user, request.Password.Trim());
            if(identityResult.Succeeded)
            {
                identityResult = await userManager.AddToRoleAsync(user, "Reader");
                if (identityResult.Succeeded)
                {
                    return Ok();
                }
            }
            foreach (var error in identityResult.Errors)
            {
                ModelState.AddModelError(error.Code, error.Description);
            }
            return ValidationProblem(ModelState);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO request)
        {
            var user = await userManager.FindByNameAsync(request.UserName.Trim());

            if (user is not null)
            {
                var result = await userManager.CheckPasswordAsync(user, request.Password.Trim());
                if(result)
                {
                    var roles = await userManager.GetRolesAsync(user);
                    var jwtToken = tokenRepository.CreateToken(user, roles);
                    LoginResponseDTO response = new()
                    {
                        UserName = request.UserName.Trim(),
                        Token = jwtToken,
                        Roles = [.. roles]
                    };
                    return Ok(response);
                }
            }

            ModelState.AddModelError("Login", "Invalid username or password");
            return ValidationProblem(ModelState);

        }

    }
}
