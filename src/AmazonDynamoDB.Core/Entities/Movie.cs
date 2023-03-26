using AmazonDynamoDB.Core.DTO.Request;

namespace AmazonDynamoDB.Core.Entities;

public class Movie
{
    public int UserId { get; private set; }
    public string MovieName { get; private set; }
    public string Description { get; private set; }
    public List<string> Actors { get; private set; }
    public int Ranking { get; private set; }
    public string RankingDateTime { get; private set; }

    

    public Movie(int userId, string movieName, string description, List<string> actors, int ranking)
    {
        UserId = userId;
        MovieName = movieName;
        Description = description;
        Actors = actors;
        Ranking = ranking;
        RankingDateTime = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
    }

    public Movie(int userId, string movieName, string description, List<string> actors, int ranking, string rankingDateTime) 
    {
        UserId = userId;
        MovieName = movieName;
        Description = description;
        Actors = actors;
        Ranking = ranking;
        RankingDateTime = rankingDateTime;
    }

    public static Movie UpdateMovie(int userId, Movie existingMovie, int ranking)
    {
        return new Movie(userId, existingMovie.MovieName, existingMovie.Description, existingMovie.Actors, ranking);
    }
}
