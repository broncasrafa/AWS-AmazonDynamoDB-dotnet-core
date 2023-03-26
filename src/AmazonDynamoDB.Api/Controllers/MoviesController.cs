using Microsoft.AspNetCore.Mvc;
using AmazonDynamoDB.Core.Interfaces.Services;
using AmazonDynamoDB.Core.DTO.Response;
using AmazonDynamoDB.Core.DTO.Request;

namespace AmazonDynamoDB.Api.Controllers;

[Route("api/movies")]
[ApiController]
public class MoviesController : ControllerBase
{
    private readonly IMovieRankService _movieRankService;

    public MoviesController(IMovieRankService movieRankService)
    {
        _movieRankService = movieRankService;
    }



    /// <summary>
    /// Get all movies
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MovieResponse>>> GetAllMoviesAsync()
    {
        var items = await _movieRankService.GetAllMoviesAsync();
        if (items.Count() == 0)
            return NotFound(new { message = "No items were found" });

        return Ok(items);
    }


    /// <summary>
    /// Get a specific movie by specifing an userId and movieName
    /// </summary>
    /// <param name="userId">user id that ranked the movie</param>
    /// <param name="movieName">movie name</param>
    [HttpGet("{userId}/{movieName}")]
    public async Task<ActionResult<MovieResponse>> GetMovieAsync(int userId, string movieName)
    {
        var result = await _movieRankService.GetMovieAsync(userId, movieName);
        if (result == null)
            return NotFound(new { message = "Movie not found" });

        return Ok(result);
    }


    /// <summary>
    /// Get users ranked movies by specifing an userId
    /// </summary>
    /// <param name="userId">user id that ranked the movie</param>
    [HttpGet("{userId}/ranked")]
    public async Task<ActionResult<IEnumerable<MovieResponse>>> GetUserRankedMoviesAsync(int userId)
    {
        var items = await _movieRankService.GetUserRankedMoviesAsync(userId);
        if (items.Count() == 0)
            return NotFound(new { message = "No items were found" });

        return Ok(items);
    }


    /// <summary>
    /// Get users ranked movies by specifing an userId and movieName startsWith
    /// </summary>
    /// <param name="userId">user id that ranked the movie</param>
    /// <param name="movieNameStartsWith">movie name that starts with</param>
    [HttpGet("{userId}/rankedMovies")]
    public async Task<ActionResult<IEnumerable<MovieResponse>>> GetUserRankedMoviesAsync(int userId, [FromQuery]string movieNameStartsWith)
    {
        if (string.IsNullOrWhiteSpace(movieNameStartsWith))
            return BadRequest(new { message = "Parameter 'movieNameStartsWith' is required." });

        var items = await _movieRankService.GetUserRankedMoviesByMovieNameStartsWithAsync(userId, movieNameStartsWith);
        if (items.Count() == 0)
            return NotFound(new { message = "No items were found" });

        return Ok(items);
    }


    /// <summary>
    /// Get movies overall ranking
    /// </summary>
    /// <param name="movieName">movie name</param>
    [HttpGet("{movieName}/ranking")]
    public async Task<ActionResult<MovieRankResponse>> GetMoviesRankingAsync(string movieName)
    {
        var result = await _movieRankService.GetMovieRankingAsync(movieName);
        return Ok(result);
    }


    /// <summary>
    /// Add a movie with a ranking
    /// </summary>
    /// <param name="userId">user id that ranked the movie</param>
    /// <param name="request">object that represents a movie</param>
    [HttpPost("{userId}")]
    public async Task<IActionResult> AddMovieAsync(int userId, [FromBody]MovieRankRequest request)
    {
        await _movieRankService.AddMovieAsync(userId, request);
        return Ok();
    }


    /// <summary>
    /// Update a movies ranking
    /// </summary>
    /// <param name="userId">user id that ranked the movie</param>
    /// <param name="request">object that represents the data for update</param>
    [HttpPatch("{userId}")]
    public async Task<IActionResult> UpdateMovieAsync(int userId, [FromBody]MovieUpdateRequest request)
    {
        await _movieRankService.UpdateMovieAsync(userId, request);
        return Ok();
    }
}
