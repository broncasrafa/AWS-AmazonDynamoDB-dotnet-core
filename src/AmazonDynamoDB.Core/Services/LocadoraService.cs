using AmazonDynamoDB.Core.DTO.Request;
using AmazonDynamoDB.Core.DTO.Response;
using AmazonDynamoDB.Core.Entities;
using AmazonDynamoDB.Core.Interfaces.Repositories;
using AmazonDynamoDB.Core.Interfaces.Services;

namespace AmazonDynamoDB.Core.Services;

public class LocadoraService : ILocadoraService
{
    private readonly ILocadoraRepository _repository;

    public LocadoraService(ILocadoraRepository repository)
    {
        _repository = repository;
    }

    

    public async Task<IEnumerable<LocadoraResponse>> GetAllTitlesAsync()
    {
        IEnumerable<Locadora> items = await _repository.GetAllTitlesAsync();
        IEnumerable<LocadoraResponse> response = LocadoraResponse.Map(items);
        return response;
    }

    public async Task<LocadoraResponse> GetTitleAsync(string titleId)
    {
        Locadora item = await _repository.GetTitleAsync(titleId);
        LocadoraResponse response = LocadoraResponse.Map(item);
        return response;
    }

    public async Task<LocadoraResponse> GetTitleAsync(string titleId, string title)
    {
        Locadora item = await _repository.GetTitleAsync(titleId, title);
        LocadoraResponse response = LocadoraResponse.Map(item);
        return response;
    }

    public async Task<IEnumerable<LocadoraResponse>> GetTitlesByCategoryAsync(string category)
    {
        IEnumerable<Locadora> response = await _repository.GetTitlesByCategoryAsync(category);
        IEnumerable<LocadoraResponse> items = LocadoraResponse.Map(response);
        return items;
    }

    public async Task<IEnumerable<LocadoraResponse>> GetTitlesByReleaseYearAsync(string releaseYear)
    {
        IEnumerable<Locadora> response = await _repository.GetTitlesByReleaseYearAsync(releaseYear);
        IEnumerable<LocadoraResponse> items = LocadoraResponse.Map(response);
        return items;
    }

    public async Task<IEnumerable<LocadoraResponse>> GetTitlesByStartsWithAsync(string titleStartsWith)
    {
        IEnumerable<Locadora> response = await _repository.GetTitlesByStartsWithAsync(titleStartsWith);
        IEnumerable<LocadoraResponse> items = LocadoraResponse.Map(response);
        return items;
    }

    public async Task<IEnumerable<LocadoraResponse>> GetTitlesByTitleNameAsync(string title)
    {
        IEnumerable<Locadora> response = await _repository.GetTitlesByTitleNameAsync(title);
        IEnumerable<LocadoraResponse> items = LocadoraResponse.Map(response);
        return items;
    }

    public async Task AddTitleAsync(LocadoraRequest request)
    {
        Locadora locadora = new Locadora(request.Title, request.Category, request.Genres, request.ReleaseYear, request.Summary);
        await _repository.AddTitleAsync(locadora);
    }

    public async Task UpdateTitleAsync(string titleId, LocadoraRequest locadoraRequest)
    {
        var existingItem = await _repository.GetTitleAsync(titleId);
        if (existingItem != null)
            throw new Exception($"Item with id {titleId} were not found.");

        Locadora locadora = Locadora.UpdateLocadora(titleId, locadoraRequest);
        await _repository.UpdateTitleAsync(locadora);
    }
}
