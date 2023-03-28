using Microsoft.AspNetCore.Mvc;
using AmazonDynamoDB.Core.Interfaces.Services;
using AmazonDynamoDB.Core.DTO.Response;
using AmazonDynamoDB.Core.DTO.Request;

namespace AmazonDynamoDB.Api.Controllers;

[Route("api/locadora")]
[ApiController]
public class LocadoraController : ControllerBase
{
    private readonly ILocadoraService _service;

    public LocadoraController(ILocadoraService service)
    {
        _service = service;
    }


    [HttpGet("titles")]
    public async Task<ActionResult<IEnumerable<LocadoraResponse>>> GetAllTitlesAsync()
    {
        var response = await _service.GetAllTitlesAsync();
        if (response.Count() == 0)
            return NotFound(new { message = "Title were not found" });

        return Ok(response);
    }


    [HttpGet("titles/{titleId}")]
    public async Task<ActionResult<LocadoraResponse>> GetTitleAsync(string titleId) 
    {
        var response = await _service.GetTitleAsync(titleId);
        if (response == null)
            return NotFound(new { message = "Title were not found" });

        return Ok(response);
    }


    [HttpGet("titles/{titleId}/{title}")]
    public async Task<ActionResult<LocadoraResponse>> GetTitleAsync(string titleId, string title)
    {
        var response = await _service.GetTitleAsync(titleId, title);
        if (response == null)
            return NotFound(new { message = "Title were not found" });

        return Ok(response);
    }


    [HttpGet("titles/begins-with")]
    public async Task<ActionResult<LocadoraResponse>> GetTitlesByStartsWithAsync([FromQuery] string startsWith)
    {
        if (string.IsNullOrWhiteSpace(startsWith))
            return BadRequest(new { message = "Parameter 'startsWith' is required" });

        var response = await _service.GetTitlesByStartsWithAsync(startsWith);
        if (response.Count() == 0)
            return NotFound(new { message = "Titles were not found" });

        return Ok(response);
    }

    [HttpGet("titles/category")]
    public async Task<ActionResult<LocadoraResponse>> GetTitlesByCategoryAsync([FromQuery] string category)
    {
        if (string.IsNullOrWhiteSpace(category))
            return BadRequest(new { message = "Parameter 'category' is required" });

        var response = await _service.GetTitlesByCategoryAsync(category);
        if (response.Count() == 0)
            return NotFound(new { message = "Titles were not found" });

        return Ok(response);
    }

    [HttpGet("titles/release-year")]
    public async Task<ActionResult<LocadoraResponse>> GetTitlesByReleaseYearAsync([FromQuery] string releaseYear)
    {
        if (string.IsNullOrWhiteSpace(releaseYear))
            return BadRequest(new { message = "Parameter 'releaseYear' is required" });

        var response = await _service.GetTitlesByReleaseYearAsync(releaseYear);
        if (response.Count() == 0)
            return NotFound(new { message = "Titles were not found" });

        return Ok(response);
    }


    [HttpGet("titles/title")]
    public async Task<ActionResult<LocadoraResponse>> GetTitlesByTitleNameAsync([FromQuery] string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            return BadRequest(new { message = "Parameter 'title' is required" });

        var response = await _service.GetTitlesByTitleNameAsync(title);
        if (response.Count() == 0)
            return NotFound(new { message = "Titles were not found" });

        return Ok(response);
    }


    [HttpPost("titles")]
    public async Task<IActionResult> AddTitleAsync([FromBody] LocadoraRequest request)
    {
        await _service.AddTitleAsync(request);
        return Ok();
    }


    [HttpPost("titles/{titleId}")]
    public async Task<IActionResult> AddMovieAsync(string titleId, [FromBody] LocadoraRequest request)
    {
        await _service.UpdateTitleAsync(titleId, request);
        return Ok();
    }
}