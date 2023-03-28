using System.Net;
using System.Text;
using AmazonDynamoDB.Infrastructure.Models;
using AmazonDynamoDB.Integration.Tests.Setup;
using Newtonsoft.Json;

namespace AmazonDynamoDB.Integration.Tests.TestsScenarios;

[Collection("api")]
public class MovieTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    readonly HttpClient _httpClient;

    public MovieTests(CustomWebApplicationFactory<Program> factory)
    {
        _httpClient = factory.CreateClient(new Microsoft.AspNetCore.Mvc.Testing.WebApplicationFactoryClientOptions { BaseAddress = new Uri("http://localhost:8000") });
    }

    [Fact]
    public async Task AddMovieRank_DataReturnsOkStatus()
    {
        const int userId = 1;

        var movieDb = new MovieDb
        {
            UserId = userId,
            MovieName = "Test-MovieName",
            Description = "Test-Description",
            Ranking = 4,
            RankingDateTime = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss"),
            Actors = new List<string> { "testActor1", "testActor2" }
        };
        var json = JsonConvert.SerializeObject(movieDb);
        var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync($"api/movies/{userId}", stringContent);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
