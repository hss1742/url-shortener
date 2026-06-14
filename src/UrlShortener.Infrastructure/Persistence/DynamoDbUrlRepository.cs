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
}