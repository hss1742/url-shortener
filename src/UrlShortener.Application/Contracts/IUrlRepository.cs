using UrlShortener.Domain.Entities;

namespace UrlShortener.Application.Contracts;

public interface IUrlRepository
{
    Task CreateAsync(ShortUrl shortUrl, CancellationToken cancellationToken = default);
}