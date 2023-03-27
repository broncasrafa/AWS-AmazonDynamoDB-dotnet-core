using AmazonDynamoDB.Core.Entities;
using AmazonDynamoDB.Core.Interfaces.Repositories;
using AmazonDynamoDB.Infrastructure.Models;
using AmazonDynamoDB.Infrastructure.Mappers;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;


namespace AmazonDynamoDB.Infrastructure.Repositories;


public class MovieRankRepository : IMovieRankRepository
{
    private readonly DynamoDBContext _context;

    public MovieRankRepository(IAmazonDynamoDB dynamoDbClient)
    {
        _context = new DynamoDBContext(dynamoDbClient);
    }    



    public async Task<Movie> GetMovieAsync(int userId, string movieName)
    {
        var data = await _context.LoadAsync<MovieDb>(userId, movieName);
        return MovieRankMapper.Map(data);
    }

    public async Task<IEnumerable<Movie>> GetAllMoviesAsync()
    {
        var data = await _context.ScanAsync<MovieDb>(new List<ScanCondition>()).GetRemainingAsync();
        return MovieRankMapper.Map(data);
    }

    public async Task<IEnumerable<Movie>> GetUserRankedMoviesAsync(int userId)
    {
        var config = new DynamoDBOperationConfig
        {
            QueryFilter = new List<ScanCondition>
            {
                new ScanCondition("UserId", ScanOperator.Equal, userId)
            }
        };
        var data = await _context.QueryAsync<MovieDb>(userId, config).GetRemainingAsync();
        return MovieRankMapper.Map(data);
    }

    public async Task<IEnumerable<Movie>> GetUserRankedMoviesByMovieNameStartsWithAsync(int userId, string movieNameStartsWith)
    {
        var config = new DynamoDBOperationConfig
        {
            QueryFilter = new List<ScanCondition>
            {
                new ScanCondition("MovieName", ScanOperator.BeginsWith, movieNameStartsWith)
            }
        };
        var data = await _context.QueryAsync<MovieDb>(userId, config).GetRemainingAsync();
        return MovieRankMapper.Map(data);
    }

    public async Task AddMovieAsync(Movie movie)
    {
        MovieDb model = MovieRankMapper.MapToModel(movie);
        await _context.SaveAsync(model);
    }

    public async Task UpdateMovieAsync(Movie movie)
    {
        MovieDb model = MovieRankMapper.MapToModel(movie);
        await _context.SaveAsync(model);
    }

    /// <summary>
    /// obter o filme e suas classificações usando o indice secundario
    /// </summary>
    /// <param name="movieName">movie name</param>
    public async Task<IEnumerable<Movie>> GetMovieRankingAsync(string movieName)
    {
        var config = new DynamoDBOperationConfig
        {
            IndexName = "MovieName-index"
        };
        var data = await _context.QueryAsync<MovieDb>(movieName, config).GetRemainingAsync(); 
        return MovieRankMapper.Map(data);
    }
}
