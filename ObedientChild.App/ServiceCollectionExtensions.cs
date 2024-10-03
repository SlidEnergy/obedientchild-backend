using Microsoft.Extensions.DependencyInjection;
using ObedientChild.App.Alice;
using ObedientChild.App.Habits;
using ObedientChild.App.Personalities;

namespace ObedientChild.App
{
    public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddObedientChildCore(this IServiceCollection services)
		{
			services.AddScoped<IUsersService, UsersService>();
			services.AddScoped<IAuthTokenService, AuthTokenService>();
			services.AddScoped<IChildrenService, ChildrenService>();
			services.AddScoped<IBalanceHistoryService, BalanceHistoryService>();
			services.AddScoped<IBalanceHistoryFactory, BalanceHistoryFactory>();
			services.AddScoped<IHabitsService, HabitsService>();
			services.AddScoped<IChildTasksService, ChildTasksService>();
			services.AddScoped<ILifeEnergyService, LifeEnergyService>();
			services.AddScoped<IAliceService, AliceService>();
			services.AddScoped<ICharacterTraitsService, CharacterTraitsService>();
			services.AddScoped<IPersonalitiesService, PersonalitiesService>();
			services.AddScoped<IBalanceService, BalanceService>();
			services.AddScoped<IDeedsService, DeedsService>();

			return services;
		}
	}
}
