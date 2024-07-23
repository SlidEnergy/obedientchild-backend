using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ObedientChild.App;
using ObedientChild.Domain;

namespace ObedientChild.Infrastructure
{
    public static class ObedientChildAppExtensions
    {
        public static IServiceCollection AddObedientChildInfrastructure(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationDbContext>(options => options
                        .UseLazyLoadingProxies()
                        .UseNpgsql(connectionString))
                    .BuildServiceProvider();

            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

            services.AddScoped<IRepository<ApplicationUser, string>, EfRepository<ApplicationUser, string>>();
            services.AddScoped<IAuthTokensRepository, EfAuthTokensRepository>();

            services.AddScoped<DataAccessLayer>();

            return services;
        }
    }
}
