using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ObedientChild.Domain
{
    public class ApplicationUser : IdentityUser, IUniqueObject<string>
    {
        public int TrusteeId { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public ApplicationUser()
        {
            TrusteeId = 1;
        }

        public override string ToString()
        {
            return $"{FirstName} {LastName} {MiddleName}";
        }
    }
}
