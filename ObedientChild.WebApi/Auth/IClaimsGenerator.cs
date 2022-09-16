using ObedientChild.Domain;
using System.Collections.Generic;
using System.Security.Claims;

namespace ObedientChild.WebApi
{
    public interface IClaimsGenerator
    {
        IEnumerable<Claim> CreateClaims(ApplicationUser user, IEnumerable<string> roles, AccessMode accessMode);
    }
}
