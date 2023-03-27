using Microsoft.AspNetCore.Mvc;
using AmazonDynamoDB.Core.Interfaces.Services;
using AmazonDynamoDB.Core.DTO.Response;
using AmazonDynamoDB.Core.DTO.Request;
using AmazonDynamoDB.Core.Services;

namespace AmazonDynamoDB.Api.Controllers;

[Route("api/students")]
[ApiController]
public class StudentGradesController : ControllerBase
{
    private readonly IStudentGradeService _service;

    public StudentGradesController(IStudentGradeService service)
    {
        _service = service;
    }



    /// <summary>
    /// Get all students
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<StudentResponse>>> GetAllStudentsAsync()
    {
        var items = await _service.GetAllStudentsAsync();
        if (items.Count() == 0)
            return NotFound(new { message = "No items were found" });

        return Ok(items);
    }


    /// <summary>
    /// Get list of grades for a specific student by studentId
    /// </summary>
    /// <param name="studentId">student id</param>
    [HttpGet("{studentId}/grades")]
    public async Task<ActionResult<IEnumerable<StudentGradeResponse>>> GetStudentGradesByIdAsync(string studentId)
    {
        var result = await _service.GetStudentGradesByIdAsync(studentId);
        if (result == null)
            return NotFound(new { message = "Student not found" });

        return Ok(result);
    }


    /// <summary>
    /// Get list of grades for a specific student and class name
    /// </summary>
    /// <param name="studentId">student id</param>
    /// <param name="className">class name</param>
    [HttpGet("{studentId}/grades/class")]
    public async Task<ActionResult<IEnumerable<StudentGradeResponse>>> GetStudentGradesByClassNameAsync(string studentId, [FromQuery]string className)
    {
        if (string.IsNullOrWhiteSpace(className))
            return BadRequest(new { message = "Classname parameter is required." });

        var result = await _service.GetStudentGradesByClassNameAsync(studentId, className);
        if (result == null)
            return NotFound(new { message = "Student not found" });

        return Ok(result);
    }


    /// <summary>
    /// Get list of grades for a specific class name
    /// </summary>
    /// <param name="className">class name</param>
    [HttpGet("grades/class")]
    public async Task<ActionResult<IEnumerable<StudentGradeResponse>>> GetStudentGradesByClassNameAsync([FromQuery] string className)
    {
        if (string.IsNullOrWhiteSpace(className))
            return BadRequest(new { message = "Classname parameter is required." });

        var result = await _service.GetGradesByClassNameAsync(className);
        if (result == null)
            return NotFound(new { message = "Student not found" });

        return Ok(result);
    }



    /// <summary>
    /// Add a student grade for an specific student
    /// </summary>
    /// <param name="studentId">student id</param>
    /// <param name="request">object that represents a student grade</param>
    [HttpPost("{studentId}/grades")]
    public async Task<IActionResult> AddGradeToSpecificStudentAsync(string studentId, [FromBody] StudentGradeRequest request)
    {
        await _service.AddGradeToSpecificStudentAsync(studentId, request);
        return Ok();
    }


    /// <summary>
    /// Add a new student
    /// </summary>
    /// <param name="request">object that represents a student grade</param>
    [HttpPost("grades")]
    public async Task<IActionResult> AddStudentGradeAsync([FromBody] StudentGradeRequest request)
    {
        await _service.AddStudentGradeAsync(request);
        return Ok();
    }


    /// <summary>
    /// Update a student grade for an specific class
    /// </summary>
    /// <param name="studentId">student id</param>
    /// <param name="request">object that represents the data for update</param>
    [HttpPatch("{studentId}")]
    public async Task<IActionResult> UpdateStudentGradeAsync(string studentId, [FromBody] StudentGradeUpdateRequest request)
    {
        await _service.UpdateStudentGradeAsync(studentId, request);
        return Ok();
    }


    /// <summary>
    /// Get the average grade of a specific student
    /// </summary>
    /// <param name="studentId">student id</param>
    [HttpGet("{studentId}/grades-average")]
    public async Task<ActionResult<StudentGradeAverageResponse>> GetStudentAverageGradeAsync(string studentId)
    {
        var result = await _service.GetStudentAverageGradeAsync(studentId);
        if (result == null)
            return NotFound(new { message = "Student not found" });

        return Ok(result);
    }
}
