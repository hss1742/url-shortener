using System.Text.Json;
using System.Text.Json.Nodes;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using UrlShortener.Application.Health;
using Microsoft.Extensions.DependencyInjection;
using UrlShortener.Api.DependencyInjection;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace UrlShortener.Api;

public class Function
{
    private static readonly IServiceProvider ServiceProvider = ServiceProviderFactory.Create();
    
    public async Task<APIGatewayProxyResponse> FunctionHandler(
    APIGatewayProxyRequest request,
    ILambdaContext context)
    {
        if (request.HttpMethod == "GET" && request.Path == "/health")
        {
            var healthService = ServiceProvider.GetRequiredService<HealthService>();
            var response = healthService.GetHealth();

            return new APIGatewayProxyResponse
            {
                StatusCode = 200,
                Body = JsonSerializer.Serialize(response)
            };
        }

        if (request.HttpMethod == "POST" && request.Path == "/shorten")
        {
            var createRequest = JsonSerializer.Deserialize<Models.CreateShortUrlRequest>(request.Body);

            return new APIGatewayProxyResponse
            {
                StatusCode = 200,
                Body = createRequest?.Url ?? "No URL"
            };
        }

        return new APIGatewayProxyResponse
        {
            StatusCode = 404,
            Body = "Route not found"
        };
    }
}