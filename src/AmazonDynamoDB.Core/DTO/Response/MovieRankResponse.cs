namespace AmazonDynamoDB.Core.DTO.Response;

public class MovieRankResponse
{
    public string MovieName { get; private set; }
    public double OverallRanking { get; private set; }

    public MovieRankResponse(string movieName, double overallRanking)
    {
        MovieName = movieName;
        OverallRanking = overallRanking;
    }
}
