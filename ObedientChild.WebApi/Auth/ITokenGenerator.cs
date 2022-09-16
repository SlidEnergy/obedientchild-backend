using Microsoft.AspNetCore.Identity;
using ObedientChild.App;
using ObedientChild.Domain;
using System.Collections.Generic;
using System.Security.Claims;

namespace ObedientChild.WebApi
{
    public interface ITokenGenerator
    {
        string GenerateAccessToken(ApplicationUser user, IEnumerable<string> roles, AccessMode accessMode);
        string GenerateAccessToken(IEnumerable<Claim> claims);
        string GenerateRefreshToken();
    }
}
