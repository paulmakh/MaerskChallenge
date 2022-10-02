using Microsoft.Extensions.DependencyInjection.Extensions;

using MaerskChallenge.Model;
using MaerskChallenge.Queue;
using MaerskChallenge.Services;
using MaerskChallenge.Middleware;

namespace MaerskChallenge.Infrastructure
{
    public static class ConfigExtensions
    {
        public static IServiceCollection AddCustomConfigs(this IServiceCollection services)
        {
            return services
                .AddRepositories()
                .AddQueues()
                .AddServices()
                .AddMiddlewares()
                .AddHostedServices();
        }

        private static IServiceCollection AddQueues(this IServiceCollection services)
        {
            services.TryAddSingleton<IQueue<Job>, JobQueue<Job>>();
            return services;
        }

        private static IServiceCollection AddMiddlewares(this IServiceCollection services)
        {
            services.AddScoped<ExceptionMiddleware>();
            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            
            services.AddTransient<ISortingService<int>, SortingService<int>>();
            return services;
        }

        private static IServiceCollection AddHostedServices(this IServiceCollection services)
        {
            services.AddHostedService<SortingBackgroundService>();
            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.TryAddSingleton<IJobRepository, JobRepository>();
            return services;
        }
    }
}
