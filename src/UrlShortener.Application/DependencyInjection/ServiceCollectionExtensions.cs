using Microsoft.Extensions.DependencyInjection;
using UrlShortener.Application.Health;

namespace UrlShortener.Application.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        services.AddScoped<HealthService>();

        return services;
    }
}