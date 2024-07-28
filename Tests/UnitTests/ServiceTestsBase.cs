using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using ObedientChild.App;
using ObedientChild.Domain;
using ObedientChild.Infrastructure;
using System;
using System.Threading.Tasks;

namespace ObedientChild.UnitTests
{
    public class TestsBase
	{
		protected readonly AutoMapperFactory _autoMapper = new AutoMapperFactory();
		protected ApplicationDbContext _db;
		protected DataAccessLayer _mockedDal;
		protected ApplicationUser _user;

		protected Mock<IRepository<ApplicationUser, string>> _users;
		protected Mock<IAuthTokensRepository> _authTokens;

		[SetUp]
		public async Task SetupBase()
		{
			var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
			optionsBuilder.UseInMemoryDatabase(TestContext.CurrentContext.Test.Name);
			_db = new ApplicationDbContext(optionsBuilder.Options);

			_users = new Mock<IRepository<ApplicationUser, string>>();
			_authTokens = new Mock<IAuthTokensRepository>();

			_mockedDal = new DataAccessLayer(_users.Object, _authTokens.Object);

			var role = new IdentityRole() { Name = Role.Admin };
			_db.Roles.Add(role);

			//var trustee = new Trustee();
			//_db.Trustee.Add(trustee);

			var email = Guid.NewGuid() + "@mail.com";
			_user = new ApplicationUser()
            {
                Trustee = new Trustee(),
                Email = email,
                UserName = email
            };
			_db.Users.Add(_user);
			
			_db.UserRoles.Add(new IdentityUserRole<string>() { RoleId = role.Id, UserId = _user.Id });

			await _db.SaveChangesAsync();
		}
	}
}
