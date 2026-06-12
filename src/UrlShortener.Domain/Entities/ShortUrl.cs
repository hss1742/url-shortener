namespace UrlShortener.Domain.Entities;

public sealed class ShortUrl
{
    public Guid Id { get; private set; }

    public string OriginalUrl { get; private set; }

    public string ShortCode { get; private set; }

    public DateTimeOffset CreatedAt { get; private set; }

    public ShortUrl(
        Guid id,
        string originalUrl,
        string shortCode,
        DateTimeOffset createdAt)
    {
        Id = id;
        OriginalUrl = originalUrl;
        ShortCode = shortCode;
        CreatedAt = createdAt;
    }
}