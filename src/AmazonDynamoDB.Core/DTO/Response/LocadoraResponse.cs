using AmazonDynamoDB.Core.Entities;

namespace AmazonDynamoDB.Core.DTO.Response;

public class LocadoraResponse
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Category { get; set; }
    public List<string> Genres { get; set; }
    public string ReleaseYear { get; set; }
    public string Summary { get; set; }

    internal static IEnumerable<LocadoraResponse> Map(IEnumerable<Locadora> items)
    {
        if(items == null)
            return Enumerable.Empty<LocadoraResponse>();

        return items.Select(item => Map(item));
    }
    internal static LocadoraResponse Map(Locadora item)
    {
        if (item == null)
            return null;

        return new LocadoraResponse
        {
            Id = item.Id,
            Title = item.Title,
            Category = item.Category,
            Genres = item.Genres,
            ReleaseYear = item.ReleaseYear,
            Summary = item.Summary,
        };
    }
}
