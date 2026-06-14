using Microsoft.Extensions.DependencyInjection;
using UrlShortener.Application.Contracts;
using UrlShortener.Infrastructure.Persistence;
using Amazon.DynamoDBv2;

namespace UrlShortener.Infrastructure.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services)
    {
        services.AddAWSService<IAmazonDynamoDB>();
        services.AddSingleton<IUrlRepository>(provider =>
        {
            var dynamoDb = provider.GetRequiredService<IAmazonDynamoDB>();

            return new DynamoDbUrlRepository(
                dynamoDb,
                Environment.GetEnvironmentVariable("URL_TABLE_NAME")!);
        });

        return services;
    }
}