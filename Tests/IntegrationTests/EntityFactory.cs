using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using ObedientChild.Domain;
using ObedientChild.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ObedientChild.WebApi.IntegrationTests
{
	public static class EntityFactoryExtensions
	{
		public static async Task<ApplicationUser> CreateUser(this ApplicationDbContext db)
		{
			var email = Guid.NewGuid().ToString();

            var user = new ApplicationUser()
            {
                Trustee = new Trustee(),
                Email = email,
                UserName = email
            };
			db.Users.Add(user);
			await db.SaveChangesAsync();

			return user;
		}
	}
}
