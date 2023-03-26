using AmazonDynamoDB.Core.Entities;

namespace AmazonDynamoDB.Core.Interfaces.Repositories;

public interface IMovieRankRepository
{
    Task<IEnumerable<Movie>> GetAllMoviesAsync();
    Task<IEnumerable<Movie>> GetUserRankedMoviesAsync(int userId);
    Task<IEnumerable<Movie>> GetUserRankedMoviesByMovieNameStartsWithAsync(int userId, string movieNameStartsWith);
    Task<IEnumerable<Movie>> GetMovieRankingAsync(string movieName);
    Task<Movie> GetMovieAsync(int userId, string movieName);    
    Task AddMovieAsync(Movie movie);
    Task UpdateMovieAsync(Movie movie);
}
