using AmazonDynamoDB.Core.Entities;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;


namespace AmazonDynamoDB.Infrastructure.Mappers;

internal static class LocadoraMapper
{
    internal static IEnumerable<Locadora> Map(ScanResponse response)
    {
        if (response == null || response.Items == null || response.Items.Count == 0)
            return Enumerable.Empty<Locadora>();

        return response.Items.Select(c => Map(c));
    }
    internal static IEnumerable<Locadora> Map(QueryResponse response)
    {
        if (response == null || response.Items == null || response.Items.Count == 0)
            return Enumerable.Empty<Locadora>();

        return response.Items.Select(c => Map(c));
    }
    internal static Locadora Map(Dictionary<string, AttributeValue> item)
    {
        return new Locadora
        (
            id: item["title_id"].S,
            title: item["title"].S,
            category: item["category"].S,
            genres: item["genre"].SS,
            releaseYear: item["release_year"].S,
            summary: item["summary"].S
        );
    }
    internal static Locadora Map(GetItemResponse response)
    {
        return new Locadora
        (
            id: response.Item["title_id"].S,
            title: response.Item["title"].S,
            category: response.Item["category"].S,
            genres: response.Item["genre"].SS,
            releaseYear: response.Item["release_year"].S,
            summary: response.Item["summary"].S
        );
    }

    internal static Dictionary<string, AttributeValue> MapToDbItem(Locadora locadora)
    {
        return new Dictionary<string, AttributeValue>
        {
            { "title_id", new AttributeValue { S = locadora.Id } },
            { "title", new AttributeValue { S = locadora.Title } },
            { "category", new AttributeValue { S = locadora.Category } },
            { "genre", new AttributeValue { SS = locadora.Genres } },
            { "release_year", new AttributeValue { S = locadora.ReleaseYear } },
            { "summary", new AttributeValue { S = locadora.Summary } },
        };
    }
    internal static Dictionary<string, AttributeValueUpdate> MapAttributeUpdates(Locadora locadora)
    {
        return new Dictionary<string, AttributeValueUpdate>
        {
            {
                "title", new AttributeValueUpdate
                {
                    Action = AttributeAction.PUT,
                    Value = new AttributeValue { S = locadora.Title }
                }
            },
            {
                "category", new AttributeValueUpdate
                {
                    Action = AttributeAction.PUT,
                    Value = new AttributeValue { S = locadora.Category }
                }
            },
            {
                "genre", new AttributeValueUpdate
                {
                    Action = AttributeAction.PUT,
                    Value = new AttributeValue { SS = locadora.Genres }
                }
            },
            {
                "release_year", new AttributeValueUpdate
                {
                    Action = AttributeAction.PUT,
                    Value = new AttributeValue { S = locadora.ReleaseYear }
                }
            },
            {
                "summary", new AttributeValueUpdate
                {
                    Action = AttributeAction.PUT,
                    Value = new AttributeValue { S = locadora.Summary }
                }
            }
        };
    }
}
