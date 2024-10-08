﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using ObedientChild.App;
using ObedientChild.Domain;
using ObedientChild.Infrastructure;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ObedientChild.WebApi.IntegrationTests
{
	public class ControllerTestBase
	{
		protected WebApiApplicationFactory<Startup> _factory;
		protected HttpClient _client;
		protected string _accessToken;
		protected string _refreshToken;
		protected ApplicationDbContext _db;
		protected UserManager<ApplicationUser> _manager;
		protected RoleManager<IdentityRole> _roleManager;
		protected ApplicationUser _user;

		[OneTimeSetUp]
		public async Task HttpClientSetup()
		{
			_factory = new WebApiApplicationFactory<Startup>();
			_client = _factory.CreateClient();
			if (_client == null)
				throw new Exception("Клиент для web api не создан.");

			var scope = _factory.Services.CreateScope();

			if (scope == null)
				throw new Exception("Область видимости scope для сервисов не создана.");

			_db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
			_manager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
			if (_db == null)
				throw new Exception("Контекст базы данных не получен");

			if (_manager == null)
				throw new Exception("Сервис для работы с пользователями не получен");

			_roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

			if (_roleManager == null)
				throw new Exception("Сервис для работы с группой пользователей не получен");

			_user = await CreateUser("test1@email.com", "Password123#");
			await CreateRole();
			await _manager.AddToRoleAsync(_user, Role.Admin);

			await Login("test1@email.com", "Password123#");
		}

		protected virtual async Task<ApplicationUser> CreateUser(string email, string password)
		{
			var user = new ApplicationUser(email);
			var result = await _manager.CreateAsync(user, password);

			if (!result.Succeeded)
				throw new Exception("Новый пользователь не создан. " + Lers.Utils.ArrayUtils.JoinToString(result.Errors.Select(x => x.Description), ", "));

			return user;
		}

		protected virtual async Task CreateRole()
		{
			var result = await _roleManager.CreateAsync(new IdentityRole(Role.Admin));

			if (!result.Succeeded)
				throw new Exception("Новая группа не создана");
		}

		protected virtual async Task Login(string email, string password)
		{
			var request = HttpRequestBuilder.CreateJsonRequest("POST", "/api/v1/users/token", null,
				new { Email = email, Password = password, ConfirmPassword = password });
			var response = await SendRequest(request);

			if (!response.IsSuccessStatusCode)
				throw new Exception("Token для нового пользователя не получен");

			var dict = await response.ToDictionary();

			if (!dict.ContainsKey("token"))
				throw new Exception("Ответ не содержит токен доступа");

			_accessToken = (string)dict["token"];

			if (_accessToken.Length <= 32)
				throw new Exception("Получен невалидный токен");

			if (!dict.ContainsKey("refreshToken"))
				throw new Exception("Ответ не содержит токен восстановления сеанса");

			_refreshToken = (string)dict["refreshToken"];

			if (_refreshToken.Length <= 32)
				throw new Exception("Получен невалидный токен");
		}

		protected virtual Task<HttpResponseMessage> SendRequest(HttpRequestMessage request) => _client.SendAsync(request, CancellationToken.None);
	}
}
