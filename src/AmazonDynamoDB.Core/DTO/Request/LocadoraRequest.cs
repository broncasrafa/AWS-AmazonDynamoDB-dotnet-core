namespace AmazonDynamoDB.Core.DTO.Request;

public class LocadoraRequest
{
    public string Title { get; set; }
    public string Category { get; set; }
    public List<string> Genres { get; set; }
    public string ReleaseYear { get; set; }
    public string Summary { get; set; }
}
