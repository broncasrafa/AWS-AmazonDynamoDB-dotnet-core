using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Amazon.Runtime;

namespace AmazonDynamoDB.Integration.Tests.Setup;

public class TestDataSetup
{
    private static string _serviceURL = "http://localhost:8000";
    
    private static AmazonDynamoDBConfig _amazonDynamoDBConfig = new AmazonDynamoDBConfig { ServiceURL = _serviceURL };
    private static BasicAWSCredentials _basicAWSCredentials = new BasicAWSCredentials("xxx", "xxx");
    private static IAmazonDynamoDB _AmazonDynamoDBClient = new AmazonDynamoDBClient(_basicAWSCredentials, _amazonDynamoDBConfig);


    public async Task CreateTableAsync()
    {
        CreateTableRequest request = new CreateTableRequest
        {
            TableName = "MovieRank",
            AttributeDefinitions = new List<AttributeDefinition>
            {
                new AttributeDefinition
                {
                    AttributeName = "UserId",
                    AttributeType = "N"
                },
                new AttributeDefinition
                {
                    AttributeName = "MovieName",
                    AttributeType = "S"
                }
            },
            KeySchema = new List<KeySchemaElement>
            {
                new KeySchemaElement
                {
                    AttributeName = "UserId",
                    KeyType = "HASH"
                },
                new KeySchemaElement
                {
                    AttributeName = "MovieName",
                    KeyType = "RANGE"
                }
            },
            ProvisionedThroughput = new ProvisionedThroughput
            {
                ReadCapacityUnits = 1,
                WriteCapacityUnits = 1
            },
            GlobalSecondaryIndexes = new List<GlobalSecondaryIndex>
            {
                new GlobalSecondaryIndex
                {
                    IndexName = "MovieName-index",
                    KeySchema = new List<KeySchemaElement>
                    {
                        new KeySchemaElement
                        {
                            AttributeName = "MovieName",
                            KeyType = "HASH"
                        }
                    },
                    ProvisionedThroughput = new ProvisionedThroughput
                    {
                        ReadCapacityUnits = 1,
                        WriteCapacityUnits = 1
                    },
                    Projection = new Projection
                    {
                        ProjectionType = "ALL"
                    }
                }
            }
        };
        await _AmazonDynamoDBClient.CreateTableAsync(request);
        await WaitUntilTableActiveAsync(request.TableName);
    }

    private static async Task WaitUntilTableActiveAsync(string tableName)
    {
        string status = null;
        do
        {
            try
            {
                status = await GetTableStatusAsync(tableName);
            }
            catch (ResourceNotFoundException)
            {

                throw;
            }
        } while (status != "ACTIVE");
    }
    private static async Task<string> GetTableStatusAsync(string tableName)
    {
        var response = await _AmazonDynamoDBClient.DescribeTableAsync(new DescribeTableRequest(tableName));
        return response.Table.TableStatus;
    }
}
