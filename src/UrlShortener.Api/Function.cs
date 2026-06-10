using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace UrlShortener.Api;

public class Function
{
    public APIGatewayProxyResponse FunctionHandler(
        APIGatewayProxyRequest request,
        ILambdaContext context)
    {
        return new APIGatewayProxyResponse
        {
            StatusCode = 200,
            Body = "URL Shortener API deployed from GitHub Actions"
        };
    }
}