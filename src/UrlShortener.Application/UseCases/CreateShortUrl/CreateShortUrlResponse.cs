namespace UrlShortener.Application.UseCases.CreateShortUrl;
public sealed record CreateShortUrlResponse(Guid Id, string ShortCode, string OriginalUrl);