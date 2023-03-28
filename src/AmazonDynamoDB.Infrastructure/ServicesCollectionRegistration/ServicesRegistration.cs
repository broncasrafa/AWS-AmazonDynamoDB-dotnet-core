using Microsoft.Extensions.DependencyInjection;
using AmazonDynamoDB.Core.Interfaces.Repositories;
using AmazonDynamoDB.Infrastructure.Repositories;


namespace AmazonDynamoDB.Infrastructure.ServicesCollectionRegistration;

public static class ServicesRegistration
{
    public static void AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddSingleton<IMovieRankRepository, MovieRankRepository>();
        services.AddSingleton<IStudentGradeRepository, StudentGradeRepository>();
        services.AddSingleton<ILocadoraRepository, LocadoraRepository>();
    }
}
