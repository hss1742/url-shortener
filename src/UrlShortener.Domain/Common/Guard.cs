using UrlShortener.Domain.Exceptions;

namespace UrlShortener.Domain.Common;

public static class Guard
{
    public static void AgainstEmptyGuid(Guid value, string paramName)
    {
        if (value == Guid.Empty)
        {
            throw new DomainException(
                $"{paramName} cannot be empty.");
        }
    }

    public static void AgainstNullOrWhiteSpace(string value, string paramName)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException(
                $"{paramName} cannot be null or whitespace.");
        }
    }
}