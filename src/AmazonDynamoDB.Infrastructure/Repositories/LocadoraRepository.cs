using AmazonDynamoDB.Core.Entities;
using AmazonDynamoDB.Core.Interfaces.Repositories;
using AmazonDynamoDB.Infrastructure.Mappers;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;


namespace AmazonDynamoDB.Infrastructure.Repositories;

public class LocadoraRepository : ILocadoraRepository
{
    private const string _TableName = "LocadoraWeb";
    private readonly IAmazonDynamoDB _dynamoDBClient;

    public LocadoraRepository(IAmazonDynamoDB dynamoDBClient)
    {
        _dynamoDBClient = dynamoDBClient;
    }


    public async Task<IEnumerable<Locadora>> GetAllTitlesAsync()
    {
        ScanRequest scanRequest = new ScanRequest(_TableName);
        ScanResponse scanResponse = await _dynamoDBClient.ScanAsync(scanRequest);
        IEnumerable<Locadora> items = LocadoraMapper.Map(scanResponse);
        return items;
    }

    public async Task<Locadora> GetTitleAsync(string titleId)
    {
        GetItemRequest request = new GetItemRequest
        {
            TableName = _TableName,
            Key = new Dictionary<string, AttributeValue>
            {
                { "title_id", new AttributeValue { S = titleId } }
            }
        };
        GetItemResponse response = await _dynamoDBClient.GetItemAsync(request);
        Locadora item = LocadoraMapper.Map(response);
        return item;
    }

    public async Task<Locadora> GetTitleAsync(string titleId, string title)
    {
        GetItemRequest request = new GetItemRequest
        {
            TableName = _TableName,
            Key = new Dictionary<string, AttributeValue>
            {
                { "title_id", new AttributeValue { S = titleId } },
                { "title", new AttributeValue { S = title } }
            }
        };
        GetItemResponse response = await _dynamoDBClient.GetItemAsync(request);
        Locadora item = LocadoraMapper.Map(response);
        return item;
    }

    public async Task<IEnumerable<Locadora>> GetTitlesByCategoryAsync(string category)
    {
        QueryRequest request = new QueryRequest
        {
            TableName = _TableName,
            KeyConditionExpression = "category = :category",
            ExpressionAttributeValues = new Dictionary<string, AttributeValue>
            {
                {
                    ":category", new AttributeValue { S = category }
                }
            }
        };
        QueryResponse response = await _dynamoDBClient.QueryAsync(request);
        IEnumerable<Locadora> items = LocadoraMapper.Map(response);
        return items;
    }

    public async Task<IEnumerable<Locadora>> GetTitlesByStartsWithAsync(string titleStartsWith)
    {
        QueryRequest request = new QueryRequest
        {
            TableName = _TableName,
            KeyConditionExpression = "begins_with (title, :titleStartsWith)",
            ExpressionAttributeValues = new Dictionary<string, AttributeValue>
            {
                { ":title", new AttributeValue { S = titleStartsWith } }
            }
        };
        QueryResponse response = await _dynamoDBClient.QueryAsync(request);
        IEnumerable<Locadora> items = LocadoraMapper.Map(response);
        return items;
    }

    public async Task<IEnumerable<Locadora>> GetTitlesByReleaseYearAsync(string releaseYear)
    {
        QueryRequest request = new QueryRequest
        {
            TableName = _TableName,
            KeyConditionExpression = "release_year = :releaseYear",
            ExpressionAttributeValues = new Dictionary<string, AttributeValue>
            {
                {
                    ":releaseYear", new AttributeValue { S = releaseYear }
                }
            }
        };
        QueryResponse response = await _dynamoDBClient.QueryAsync(request);
        IEnumerable<Locadora> items = LocadoraMapper.Map(response);
        return items;
    }

    public async Task<IEnumerable<Locadora>> GetTitlesByTitleNameAsync(string title)
    {
        QueryRequest request = new QueryRequest
        { 
            TableName = _TableName, 
            IndexName = "title-index",
            KeyConditionExpression = "title = :title",
            ExpressionAttributeValues = new Dictionary<string, AttributeValue>
            {
                { ":title", new AttributeValue { S = title} }
            }
        };
        QueryResponse response = await _dynamoDBClient.QueryAsync(request);
        IEnumerable<Locadora> items = LocadoraMapper.Map(response);
        return items;
    }

    public async Task AddTitleAsync(Locadora locadora)
    {
        PutItemRequest request = new PutItemRequest
        {
            TableName = _TableName,
            Item = LocadoraMapper.MapToDbItem(locadora)
        };
        await _dynamoDBClient.PutItemAsync(request);
    }

    public async Task UpdateTitleAsync(Locadora locadora)
    {
        UpdateItemRequest request = new UpdateItemRequest
        {
            TableName = _TableName,
            Key = new Dictionary<string, AttributeValue>
            {
                { "title_id", new AttributeValue { S = locadora.Id } }
            },
            AttributeUpdates = LocadoraMapper.MapAttributeUpdates(locadora)
        };
        await _dynamoDBClient.UpdateItemAsync(request);
    }
}
