﻿using Microsoft.Extensions.DependencyInjection;

namespace ObedientChild.App
{
    public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddObedientChildCore(this IServiceCollection services)
		{
			services.AddScoped<IUsersService, UsersService>();
			services.AddScoped<IAuthTokenService, AuthTokenService>();
			services.AddScoped<IChildrenService, ChildrenService>();
			services.AddScoped<IRewardsService, RewardsService>();

			return services;
		}
	}
}