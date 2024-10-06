using Microsoft.AspNetCore.Identity;
using Moq;
using NUnit.Framework;
using ObedientChild.App;
using ObedientChild.Domain;
using System;
using System.Threading.Tasks;
using ObedientChild.WebApi;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ObedientChild.UnitTests
{
	public class AuthTokenServiceTests : TestsBase
	{
		private AuthTokenService _service;

		[SetUp]
        public void Setup()
        {
			_service = new AuthTokenService(_db);
        }

		[Test]
		[TestCase(AuthTokenType.RefreshToken)]
		[TestCase(AuthTokenType.TelegramUserId)]
		public async Task AddToken_ShouldNotBeException(AuthTokenType type)
		{
			_db.Users.Add(_user);

			var token = Guid.NewGuid().ToString();

			await _service.AddOrUpdateTokenAsync(_user.Id, token, type);

			var authToken = await _db.AuthTokens.SingleOrDefaultAsync(x => x.Token == token && x.Type == type && x.UserId == _user.Id);
			Assert.That(authToken != null);
		}
	}
}