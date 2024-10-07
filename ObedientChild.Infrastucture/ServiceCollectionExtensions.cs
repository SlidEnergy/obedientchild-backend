using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ObedientChild.App;
using ObedientChild.Domain;
using ObedientChild.Infrastructure.GoogleAuth;
using ObedientChild.Infrastructure.SearchImages;

namespace ObedientChild.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddObedientChildInfrastructure(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationDbContext>(options => options
                        .UseLazyLoadingProxies()
                        .UseNpgsql(connectionString))
                    .BuildServiceProvider();

            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

            return services;
        }

        public static IServiceCollection AddSearchImages(this IServiceCollection services, SerpapiOptions serpapiOptions)
        {
            services.AddTransient<ISearchImageService, SearchImageService>();
            services.AddSingleton(serpapiOptions);

            return services;
        }

        public static IServiceCollection AddGoogleAuth(this IServiceCollection services, GoogleAuthOptions options)
        {
            services.AddTransient<IGoogleTokenService, GoogleTokenService>();
            services.AddSingleton(options);

            return services;
        }
    }
}
