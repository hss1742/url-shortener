using UrlShortener.Application.Contracts;
using UrlShortener.Domain.Entities;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;

namespace UrlShortener.Infrastructure.Persistence;

public sealed class DynamoDbUrlRepository : IUrlRepository
{
    private readonly IAmazonDynamoDB _dynamoDb;
    private readonly string _tableName;

    public DynamoDbUrlRepository(IAmazonDynamoDB dynamoDb, string tableName)
    {
        _dynamoDb = dynamoDb;
        _tableName = tableName;
    }
    public async Task CreateAsync(ShortUrl shortUrl, CancellationToken cancellationToken = default)
    {
        var item = new Dictionary<string, AttributeValue>
        {
            ["ShortCode"] = new AttributeValue { S = shortUrl.ShortCode},
            ["Id"] = new AttributeValue { S = shortUrl.Id.ToString() },
            ["OriginalUrl"] = new AttributeValue { S = shortUrl.OriginalUrl },
            ["CreatedAt"] = new AttributeValue { S = shortUrl.CreatedAt.ToString("O") }
        };

        await _dynamoDb.PutItemAsync(
        new PutItemRequest
        {
            TableName = _tableName,
            Item = item
        },
        cancellationToken);
    }

    public async Task<ShortUrl?> GetByShortCodeAsync(string shortCode, CancellationToken cancellationToken)
    {
        var request = new GetItemRequest
        {
            TableName = _tableName,
            Key = new Dictionary<string, AttributeValue>
            {
                ["ShortCode"] = new AttributeValue
                {
                    S = shortCode
                }
            }
        };

        var response = await _dynamoDb.GetItemAsync(request,cancellationToken);
        if (response.Item.Count == 0)
        {
            return null;
        }

        var item = response.Item;
        var id = Guid.Parse(item["Id"].S);
        var originalUrl = item["OriginalUrl"].S!;
        var storedShortCode = item["ShortCode"].S!;
        var createdAt = DateTimeOffset.Parse(item["CreatedAt"].S!, null, System.Globalization.DateTimeStyles.RoundtripKind);

        return ShortUrl.Rehydrate(id, originalUrl, storedShortCode, createdAt);
    }
}