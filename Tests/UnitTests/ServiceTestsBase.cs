﻿using Microsoft.EntityFrameworkCore;
using Moq;
using ObedientChild.App;
using ObedientChild.Infrastructure;
using NUnit.Framework;
using System.Threading.Tasks;
using ObedientChild.Domain;
using Microsoft.AspNetCore.Identity;
using System;

namespace ObedientChild.UnitTests
{
	public class TestsBase
	{
		protected readonly AutoMapperFactory _autoMapper = new AutoMapperFactory();
		protected ApplicationDbContext _db;
		protected ApplicationUser _user;

		[SetUp]
		public async Task SetupBase()
		{
			var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
			optionsBuilder.UseInMemoryDatabase(TestContext.CurrentContext.Test.Name);
			_db = new ApplicationDbContext(optionsBuilder.Options);

			var role = new IdentityRole() { Name = Role.Admin };
			_db.Roles.Add(role);

			//var trustee = new Trustee();
			//_db.Trustee.Add(trustee);

			var userName = Guid.NewGuid() + "@mail.com";
			_user = new ApplicationUser(userName);
			_db.Users.Add(_user);
			
			_db.UserRoles.Add(new IdentityUserRole<string>() { RoleId = role.Id, UserId = _user.Id });

			await _db.SaveChangesAsync();
		}
	}
}
