namespace UrlShortener.Application.Health;

public sealed class HealthService
{
    public HealthResponse GetHealth()
    {
        return new HealthResponse
        {
            Message = "URL Shortener API deployed from GitHub Actions"
        };
    }
}