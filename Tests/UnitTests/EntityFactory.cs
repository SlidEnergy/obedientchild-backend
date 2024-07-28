using ObedientChild.Domain;
using ObedientChild.Infrastructure;
using System;
using System.Threading.Tasks;

namespace ObedientChild.UnitTests
{
    public static class EntityFactoryExtensions
	{
		public static async Task<ApplicationUser> CreateUser(this ApplicationDbContext db)
		{
			var email = Guid.NewGuid().ToString();

            var user = new ApplicationUser()
            {
                Email = email,
                UserName = email
            };
			db.Users.Add(user);
			await db.SaveChangesAsync();

			return user;
		}
	}
}
