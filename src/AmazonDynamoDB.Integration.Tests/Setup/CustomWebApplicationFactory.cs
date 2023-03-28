using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Amazon.DynamoDBv2;

namespace AmazonDynamoDB.Integration.Tests.Setup;

public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
            services.AddSingleton<IAmazonDynamoDB>(config =>
            {
                var clientConfig = new AmazonDynamoDBConfig { ServiceURL = "http://localhost:8000" };
                return new AmazonDynamoDBClient(clientConfig);
            }));
    }
}
