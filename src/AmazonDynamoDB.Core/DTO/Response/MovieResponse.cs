using AmazonDynamoDB.Core.Entities;

namespace AmazonDynamoDB.Core.DTO.Response;

public class MovieResponse
{
    public string MovieName { get; private set; }
    public string Description { get; private set; }
    public List<string> Actors { get; private set; }
    public int Ranking { get; private set; }
    public string RankedAt { get; private set; }

    public MovieResponse(string movieName, string description, List<string> actors, int ranking, string rankedAt)
    {
        MovieName = movieName;
        Description = description;
        Actors = actors;
        Ranking = ranking;
        RankedAt = rankedAt;
    }

    public static IEnumerable<MovieResponse> Map(IEnumerable<Movie> movies)
    {
        if (movies == null)
            return Enumerable.Empty<MovieResponse>();
        return movies.Select(c => Map(c));
    }
    public static MovieResponse Map(Movie movie)
    {
        if (movie == null) 
            return null;

        return new MovieResponse(movie.MovieName, movie.Description, movie.Actors, movie.Ranking, movie.RankingDateTime);
    }
}
