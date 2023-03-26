using AmazonDynamoDB.Core.DTO.Request;
using AmazonDynamoDB.Core.DTO.Response;

namespace AmazonDynamoDB.Core.Interfaces.Services;

public interface IMovieRankService
{
    Task<IEnumerable<MovieResponse>> GetAllMoviesAsync();
    Task<IEnumerable<MovieResponse>> GetUserRankedMoviesAsync(int userId);
    Task<IEnumerable<MovieResponse>> GetUserRankedMoviesByMovieNameStartsWithAsync(int userId, string movieNameStartsWith);
    Task<MovieResponse> GetMovieAsync(int userId, string movieName);
    Task<MovieRankResponse> GetMovieRankingAsync(string movieName);
    Task AddMovieAsync(int userId, MovieRankRequest request);
    Task UpdateMovieAsync(int userId, MovieUpdateRequest request);
}
