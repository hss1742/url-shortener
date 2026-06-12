using UrlShortener.Application.Contracts;
using UrlShortener.Domain.Entities;

namespace UrlShortener.Application.UseCases.CreateShortUrl;

public sealed class CreateShortUrlHandler
{
    private readonly IUrlRepository _repository;

    public CreateShortUrlHandler(IUrlRepository repository)
    {
        _repository = repository;
    }

    public async Task<CreateShortUrlResponse> HandleAsync(CreateShortUrlRequest request, CancellationToken cancellationToken = default)
    {
        var shortCode = GenerateShortCode();

        var shortUrl = ShortUrl.Create(
            request.Url,
            shortCode);

        await _repository.CreateAsync(
            shortUrl,
            cancellationToken);

        return new CreateShortUrlResponse(
            shortUrl.Id,
            shortUrl.ShortCode,
            shortUrl.OriginalUrl);
    }

    private static string GenerateShortCode()
    {
        return Guid.NewGuid()
            .ToString("N")
            .Substring(0, 6);
    }
}