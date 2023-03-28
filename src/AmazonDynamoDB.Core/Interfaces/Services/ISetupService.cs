namespace AmazonDynamoDB.Core.Interfaces.Services;

public interface ISetupService
{
    Task CreateDynamoDBTableAsync(string tableName);
    Task DeleteDynamoDBTableAsync(string tableName);
}