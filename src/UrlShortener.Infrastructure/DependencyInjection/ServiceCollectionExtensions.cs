using Microsoft.Extensions.DependencyInjection;
using UrlShortener.Application.Contracts;
using UrlShortener.Infrastructure.Persistence;

namespace UrlShortener.Infrastructure.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services)
    {
        services.AddSingleton<IUrlRepository, InMemoryUrlRepository>();

        return services;
    }
}