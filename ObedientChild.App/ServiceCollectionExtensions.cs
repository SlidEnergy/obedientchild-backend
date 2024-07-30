using Microsoft.Extensions.DependencyInjection;
using ObedientChild.App.Habbits;

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
			services.AddScoped<IGoodDeedService, GoodDeedService>();
			services.AddScoped<IBadDeedService, BadDeedService>();
			services.AddScoped<ICoinHistoryService, CoinHistoryService>();
			services.AddScoped<ICoinHistoryFactory, CoinHistoryFactory>();
			services.AddScoped<IHabbitsService, HabbitsService>();

			return services;
		}
	}
}
