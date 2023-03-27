using AmazonDynamoDB.Core.Entities;
using AmazonDynamoDB.Infrastructure.Models;

namespace AmazonDynamoDB.Infrastructure.Mappers;

internal static class MovieRankMapper
{
    public static IEnumerable<Movie> Map(IEnumerable<MovieDb> models)
    {
        if (models == null)
            return Enumerable.Empty<Movie>();

        return models.Select(c => Map(c));
    }
    public static Movie Map(MovieDb model)
    {
        if (model == null) return null;

        return new Movie(model.UserId, model.MovieName, model.Description, model.Actors, model.Ranking, model.RankingDateTime);
    }
    public static MovieDb MapToModel(Movie entity)
    {
        if (entity == null) return null;
        return new MovieDb
        {
            UserId = entity.UserId,
            MovieName = entity.MovieName,
            Description = entity.Description,
            Actors = entity.Actors,
            Ranking = entity.Ranking,
            RankingDateTime = entity.RankingDateTime,
        };
    }
}
