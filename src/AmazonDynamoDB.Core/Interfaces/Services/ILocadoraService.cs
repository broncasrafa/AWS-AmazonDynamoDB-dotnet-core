using AmazonDynamoDB.Core.DTO.Request;
using AmazonDynamoDB.Core.DTO.Response;

namespace AmazonDynamoDB.Core.Interfaces.Services;

public interface ILocadoraService
{
    Task<IEnumerable<LocadoraResponse>> GetAllTitlesAsync();
    Task<LocadoraResponse> GetTitleAsync(string titleId);
    Task<LocadoraResponse> GetTitleAsync(string titleId, string title);
    Task<IEnumerable<LocadoraResponse>> GetTitlesByStartsWithAsync(string titleStartsWith);
    Task<IEnumerable<LocadoraResponse>> GetTitlesByCategoryAsync(string category);
    Task<IEnumerable<LocadoraResponse>> GetTitlesByReleaseYearAsync(string releaseYear);
    Task<IEnumerable<LocadoraResponse>> GetTitlesByTitleNameAsync(string title);
    Task AddTitleAsync(LocadoraRequest locadoraRequest);
    Task UpdateTitleAsync(string titleId, LocadoraRequest locadoraRequest);
}
