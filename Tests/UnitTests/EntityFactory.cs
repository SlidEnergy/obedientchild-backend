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
			var user = new ApplicationUser(Guid.NewGuid().ToString());
			db.Users.Add(user);
			await db.SaveChangesAsync();

			return user;
		}
	}
}
