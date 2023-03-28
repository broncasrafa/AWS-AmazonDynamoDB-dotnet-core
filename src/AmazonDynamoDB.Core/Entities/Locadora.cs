using AmazonDynamoDB.Core.DTO.Request;

namespace AmazonDynamoDB.Core.Entities;

public class Locadora
{
    public string Id { get; private set; }
    public string Title { get; private set; }
    public string Category { get; private set; }
    public List<string> Genres { get; private set; }
    public string ReleaseYear { get; private set; }
    public string Summary { get; private set; }

    public Locadora(string title, string category, List<string> genres, string releaseYear, string summary)
    {
        Id = Guid.NewGuid().ToString();
        Title = title;
        Category = category;
        Genres = genres;
        ReleaseYear = releaseYear;
        Summary = summary;
    }

    public Locadora(string id, string title, string category, List<string> genres, string releaseYear, string summary)
    {
        Id = id;
        Title = title;
        Category = category;
        Genres = genres;
        ReleaseYear = releaseYear;
        Summary = summary;
    }

    public static Locadora UpdateLocadora(string titleId, LocadoraRequest request)
    {
        return new Locadora(titleId, request.Title, request.Category, request.Genres, request.ReleaseYear, request.Summary);
    }
}
