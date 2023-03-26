using AmazonDynamoDB.Core.DTO.Request;
using AmazonDynamoDB.Core.DTO.Response;
using AmazonDynamoDB.Core.Entities;
using AmazonDynamoDB.Core.Interfaces.Repositories;
using AmazonDynamoDB.Core.Interfaces.Services;

namespace AmazonDynamoDB.Core.Services;

public class MovieRankService : IMovieRankService
{
    private readonly IMovieRankRepository _repository;

    public MovieRankService(IMovieRankRepository repository)
    {
        _repository = repository;
    }



    public async Task<IEnumerable<MovieResponse>> GetAllMoviesAsync()
    {
        var response = await _repository.GetAllMoviesAsync();
        var result = MovieResponse.Map(response);
        return result;
    }

    public async Task<IEnumerable<MovieResponse>> GetUserRankedMoviesAsync(int userId)
    {
        var response = await _repository.GetUserRankedMoviesAsync(userId);
        var result = MovieResponse.Map(response);
        return result;
    }

    public async Task<IEnumerable<MovieResponse>> GetUserRankedMoviesByMovieNameStartsWithAsync(int userId, string movieNameStartsWith)
    {
        var response = await _repository.GetUserRankedMoviesByMovieNameStartsWithAsync(userId, movieNameStartsWith);
        var result = MovieResponse.Map(response);
        return result;
    }

    public async Task<MovieResponse> GetMovieAsync(int userId, string movieName)
    {
        var response = await _repository.GetMovieAsync(userId, movieName);
        var result = MovieResponse.Map(response);
        return result;
    }

    public async Task<MovieRankResponse> GetMovieRankingAsync(string movieName)
    {
        var response = await _repository.GetMovieRankingAsync(movieName);
        var movies = MovieResponse.Map(response);
        var overallMovieRanking = movies.Select(c => c.Ranking).Average();
        return new MovieRankResponse(movieName, overallMovieRanking);
    }

    public async Task AddMovieAsync(int userId, MovieRankRequest request)
    {
        var movie = new Movie(userId, request.MovieName, request.Description, request.Actors, request.Ranking);
        await _repository.AddMovieAsync(movie);
    }

    public async Task UpdateMovieAsync(int userId, MovieUpdateRequest request)
    {
        var existingRecord = await _repository.GetMovieAsync(userId, request.MovieName);
        var movie = Movie.UpdateMovie(userId, existingRecord, request.Ranking);
        await _repository.UpdateMovieAsync(movie);
    }
}
