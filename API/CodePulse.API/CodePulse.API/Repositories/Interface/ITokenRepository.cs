﻿using Microsoft.AspNetCore.Identity;

namespace CodePulse.API.Repositories.Interface
{
    public interface ITokenRepository
    {
        string CreateToken(IdentityUser user, IList<string> roles);
    }
}
