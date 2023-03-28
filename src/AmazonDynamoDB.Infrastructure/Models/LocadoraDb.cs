using Amazon.DynamoDBv2.DataModel;

namespace AmazonDynamoDB.Infrastructure.Models;

[DynamoDBTable("LocadoraWeb")]
public class LocadoraDb
{
    [DynamoDBHashKey("title_id")]
    public string TitleId { get; set; }

    [DynamoDBGlobalSecondaryIndexHashKey]
    [DynamoDBProperty("title")]
    public string Title { get; set; }

    [DynamoDBProperty("category")]
    public string Category { get; set; }

    [DynamoDBProperty("genre")]
    public List<string> Genre { get; set; }

    [DynamoDBProperty("release_year")]
    public int ReleaseYear { get; set; }

    [DynamoDBProperty("summary")]
    public string Summary { get; set; }
}
