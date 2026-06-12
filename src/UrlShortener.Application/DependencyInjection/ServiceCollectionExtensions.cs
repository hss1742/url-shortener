using Microsoft.Extensions.DependencyInjection;
using UrlShortener.Application.Health;
using UrlShortener.Application.UseCases.CreateShortUrl;

namespace UrlShortener.Application.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        services.AddScoped<HealthService>();
        services.AddScoped<CreateShortUrlHandler>();

        return services;
    }
}