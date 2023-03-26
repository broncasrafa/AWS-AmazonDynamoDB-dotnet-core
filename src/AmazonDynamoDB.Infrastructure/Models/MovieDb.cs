using Amazon.DynamoDBv2.DataModel;

namespace AmazonDynamoDB.Infrastructure.Models;

[DynamoDBTable("MovieRank")]
internal class MovieDb
{
    [DynamoDBHashKey]
    public int UserId { get; set; }

    [DynamoDBGlobalSecondaryIndexHashKey]
    public string MovieName { get; set; }

    public string Description { get; set; }
    public List<string> Actors { get; set; }
    public int Ranking { get; set; }
    public string RankingDateTime { get; set; }
}
