using UrlShortener.Domain.Common;

namespace UrlShortener.Domain.Entities;

public sealed class ShortUrl
{
    public Guid Id { get; private set; }

    public string OriginalUrl { get; private set; }

    public string ShortCode { get; private set; }

    public DateTimeOffset CreatedAt { get; private set; }

    private ShortUrl(Guid id, string originalUrl, string shortCode, DateTimeOffset createdAt)
    {
        Guard.AgainstEmptyGuid(id, nameof(id));
        Guard.AgainstNullOrWhiteSpace(
            originalUrl,
            nameof(originalUrl));

        Guard.AgainstNullOrWhiteSpace(
            shortCode,
            nameof(shortCode));

        Id = id;
        OriginalUrl = originalUrl;
        ShortCode = shortCode;
        CreatedAt = createdAt;
    }

    public static ShortUrl Create(string originalUrl, string shortCode)
    {
        return new ShortUrl(
            Guid.NewGuid(),
            originalUrl,
            shortCode,
            DateTimeOffset.UtcNow);
    }
}