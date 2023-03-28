using AmazonDynamoDB.Core.Interfaces.Repositories;
using AmazonDynamoDB.Core.Interfaces.Services;

namespace AmazonDynamoDB.Core.Services;


public class SetupService : ISetupService
{
    private readonly ISetupRepository _repository;

    public SetupService(ISetupRepository repository)
    {
        _repository = repository;
    }




    public async Task CreateDynamoDBTableAsync(string tableName)
    {
        await _repository.CreateDynamoDBTableAsync(tableName);
    }

    public async Task DeleteDynamoDBTableAsync(string tableName)
    {
        await _repository.DeleteDynamoDBTableAsync(tableName);
    }
}