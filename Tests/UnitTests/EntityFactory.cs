using ObedientChild.Domain;
using ObedientChild.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ObedientChild.UnitTests
{
	public static class EntityFactoryExtensions
	{
		public static async Task<ApplicationUser> CreateUser(this ApplicationDbContext db)
		{
			var trustee = new Trustee();
			db.Trustee.Add(trustee);
			await db.SaveChangesAsync();
			var user = new ApplicationUser(trustee, Guid.NewGuid().ToString());
			db.Users.Add(user);
			await db.SaveChangesAsync();

			return user;
		}
	}
}
