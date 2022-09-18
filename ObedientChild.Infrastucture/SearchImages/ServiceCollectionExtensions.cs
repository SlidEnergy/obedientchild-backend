using Microsoft.Extensions.DependencyInjection;

namespace ObedientChild.Infrastructure.SearchImages
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSearchImages(this IServiceCollection services, SerpapiOptions serpapiOptions)
        {
            services.AddTransient<ISearchImageService, SearchImageService>();
            services.AddSingleton(serpapiOptions);

            return services;
        }
    }
}
