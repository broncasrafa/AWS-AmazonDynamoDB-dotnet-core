using AmazonDynamoDB.Core.Interfaces.Repositories;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;


namespace AmazonDynamoDB.Infrastructure.Repositories;

public class SetupRepository : ISetupRepository
{
    private readonly IAmazonDynamoDB _dynamoDBClient;

    public SetupRepository(IAmazonDynamoDB dynamoDBClient)
    {
        _dynamoDBClient = dynamoDBClient;
    }


    public async Task CreateDynamoDBTableAsync(string tableName)
    {
        CreateTableRequest request = new CreateTableRequest
        {
            TableName = tableName,
            AttributeDefinitions = new List<AttributeDefinition>
            {
                new AttributeDefinition
                {
                    AttributeName = "Id",
                    AttributeType = "S"
                }
            },
            KeySchema = new List<KeySchemaElement> 
            { 
                new KeySchemaElement
                {
                    AttributeName = "Id",
                    KeyType = "HASH"
                }
            },
            ProvisionedThroughput = new ProvisionedThroughput
            {
                ReadCapacityUnits = 1,
                WriteCapacityUnits = 1
            }
        };
        await _dynamoDBClient.CreateTableAsync(request);
    }

    public async Task DeleteDynamoDBTableAsync(string tableName)
    {
        DeleteTableRequest request = new DeleteTableRequest(tableName);
        await _dynamoDBClient.DeleteTableAsync(request);
    }
}