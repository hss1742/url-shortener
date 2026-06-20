using UrlShortener.Application.Contracts;
using UrlShortener.Domain.Entities;

namespace UrlShortener.Infrastructure.Persistence;

public sealed class InMemoryUrlRepository : IUrlRepository
{
    private readonly List<ShortUrl> _urls = [];

    public Task CreateAsync(ShortUrl shortUrl, CancellationToken cancellationToken = default)
    {
        _urls.Add(shortUrl);
        return Task.CompletedTask;
    }

    public Task<ShortUrl?> GetByShortCodeAsync(string shortCode, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}