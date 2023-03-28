using AmazonDynamoDB.Core.Entities;

namespace AmazonDynamoDB.Core.Interfaces.Repositories;

public interface ILocadoraRepository
{
    Task<IEnumerable<Locadora>> GetAllTitlesAsync();
    Task<Locadora> GetTitleAsync(string titleId);
    Task<Locadora> GetTitleAsync(string titleId, string title);
    Task<IEnumerable<Locadora>> GetTitlesByStartsWithAsync(string titleStartsWith);
    Task<IEnumerable<Locadora>> GetTitlesByCategoryAsync(string category);
    Task<IEnumerable<Locadora>> GetTitlesByReleaseYearAsync(string releaseYear);
    Task<IEnumerable<Locadora>> GetTitlesByTitleNameAsync(string title);
    Task AddTitleAsync(Locadora locadora);
    Task UpdateTitleAsync(Locadora locadora);
}
