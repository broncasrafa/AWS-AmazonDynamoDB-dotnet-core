namespace AmazonDynamoDB.Core.Interfaces.Repositories;

public interface ISetupRepository
{
    Task CreateDynamoDBTableAsync(string tableName);
    Task DeleteDynamoDBTableAsync(string tableName);
}