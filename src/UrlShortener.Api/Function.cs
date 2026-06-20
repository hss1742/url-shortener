using System.Text.Json;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using UrlShortener.Application.Health;
using Microsoft.Extensions.DependencyInjection;
using UrlShortener.Api.DependencyInjection;
using UrlShortener.Application.UseCases.CreateShortUrl;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace UrlShortener.Api;

public class Function
{
    private static readonly IServiceProvider ServiceProvider = ServiceProviderFactory.Create();
    
    public async Task<APIGatewayHttpApiV2ProxyResponse> FunctionHandler(APIGatewayHttpApiV2ProxyRequest request, ILambdaContext context)
    {
        var method = request.RequestContext.Http.Method;
        var path = request.RawPath;
        context.Logger.LogInformation($"Method: {method}, Path: {path}");

        if (method == "GET" && path == "/health")
        {
            var healthService = ServiceProvider.GetRequiredService<HealthService>();
            var response = healthService.GetHealth();

            return new APIGatewayHttpApiV2ProxyResponse
            {
                StatusCode = 200,
                Body = JsonSerializer.Serialize(response)
            };
        }

        if (method == "GET" && path != "/health")
        {
            var shortCode = path.Trim('/');

            return new APIGatewayHttpApiV2ProxyResponse
            {
                StatusCode = 200,
                Body = $"Short code: {shortCode}"
            };
        }

        if (method == "POST" && path == "/shorten")
        {
            var createRequest = JsonSerializer.Deserialize<Models.CreateShortUrlRequest>(request.Body);

            var handler = ServiceProvider.GetRequiredService<CreateShortUrlHandler>();

            var response = await handler.HandleAsync(
                new CreateShortUrlRequest(createRequest!.Url));

            return new APIGatewayHttpApiV2ProxyResponse
            {
                StatusCode = 200,
                Body = JsonSerializer.Serialize(response)
            };
        }

        return new APIGatewayHttpApiV2ProxyResponse
        {
            StatusCode = 404,
            Body = "Router Not found."
        };
    }
}