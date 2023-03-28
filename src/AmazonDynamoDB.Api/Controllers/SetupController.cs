using Microsoft.AspNetCore.Mvc;
using AmazonDynamoDB.Core.Interfaces.Services;


namespace AmazonDynamoDB.Api.Controllers;

[Route("api/setup")]
[ApiController]
public class SetupController : ControllerBase
{
    private readonly ISetupService _setupService;

    public SetupController(ISetupService setupService)
    {
        _setupService = setupService;
    }

    [HttpPost("table/{tableName}")]
    public async Task<IActionResult> CreateDynamoDBTableAsync(string tableName)
    {
        await _setupService.CreateDynamoDBTableAsync(tableName);
        return Ok(true);
    }

    [HttpDelete("table/{tableName}")]
    public async Task<IActionResult> DeleteDynamoDBTableAsync(string tableName)
    {
        await _setupService.DeleteDynamoDBTableAsync(tableName);
        return Ok(true);
    }
}
