using Microsoft.Extensions.DependencyInjection;
using UrlShortener.Application.DependencyInjection;
using UrlShortener.Infrastructure.DependencyInjection;

namespace UrlShortener.Api.DependencyInjection;

public static class ServiceProviderFactory
{
    public static IServiceProvider Create()
    {
        var services = new ServiceCollection();

        services.AddApplication();
        services.AddInfrastructure();

        return services.BuildServiceProvider();
    }
}