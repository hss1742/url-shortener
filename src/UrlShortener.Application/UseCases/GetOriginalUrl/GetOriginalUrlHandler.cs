using UrlShortener.Application.Contracts;

namespace UrlShortener.Application.UseCases.GetOriginalUrl;

public sealed class GetOriginalUrlHandler
{
    private readonly IUrlRepository _urlRepository;

    public GetOriginalUrlHandler(IUrlRepository urlRepository)
    {
        _urlRepository = urlRepository;
    }

    public async Task<GetOriginalUrlResponse?> HandleAsync(
        GetOriginalUrlRequest request,
        CancellationToken cancellationToken)
    {
        var shortUrl = await _urlRepository.GetByShortCodeAsync(request.ShortCode, cancellationToken);
        if(shortUrl is null)
        {
            return null;
        }

        return new GetOriginalUrlResponse(shortUrl.OriginalUrl);
    }
}