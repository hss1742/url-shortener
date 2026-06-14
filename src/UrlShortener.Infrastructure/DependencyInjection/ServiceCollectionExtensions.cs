using Microsoft.Extensions.DependencyInjection;
using UrlShortener.Application.Contracts;
using UrlShortener.Infrastructure.Persistence;
using Amazon.DynamoDBv2;
using UrlShortener.Infrastructure.Configuration;

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
            var options = new DynamoDbOptions
            {
                TableName = Environment.GetEnvironmentVariable("URL_TABLE_NAME")!
            };

            return new DynamoDbUrlRepository(
                dynamoDb,
                options.TableName);
        });

        return services;
    }
}