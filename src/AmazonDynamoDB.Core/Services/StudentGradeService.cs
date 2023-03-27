using AmazonDynamoDB.Core.DTO.Request;
using AmazonDynamoDB.Core.DTO.Response;
using AmazonDynamoDB.Core.Entities;
using AmazonDynamoDB.Core.Interfaces.Repositories;
using AmazonDynamoDB.Core.Interfaces.Services;

namespace AmazonDynamoDB.Core.Services;

public class StudentGradeService : IStudentGradeService
{
    private readonly IStudentGradeRepository _repository;

    public StudentGradeService(IStudentGradeRepository repository)
    {
        _repository = repository;
    }


    public async Task<IEnumerable<StudentResponse>> GetAllStudentsAsync()
    {
        IEnumerable<StudentGrade> items = await _repository.GetAllStudentsAsync();
        IEnumerable<StudentResponse> result = StudentResponse.Map(items);
        return result;
    }

    public async Task<IEnumerable<StudentGradeResponse>> GetStudentGradesByIdAsync(string studentId)
    {
        IEnumerable<StudentGrade> item = await _repository.GetStudentGradesByIdAsync(studentId);
        IEnumerable<StudentGradeResponse> result = StudentGradeResponse.Map(item);
        return result;
    }

    public async Task<IEnumerable<StudentGradeResponse>> GetStudentGradesByClassNameAsync(string studentId, string className)
    {
        IEnumerable<StudentGrade> items = await _repository.GetStudentGradesByClassNameAsync(studentId, className);
        IEnumerable<StudentGradeResponse> result = StudentGradeResponse.Map(items);
        return result;
    }

    public async Task<IEnumerable<StudentGradeResponse>> GetGradesByClassNameAsync(string className)
    {
        IEnumerable<StudentGrade> items = await _repository.GetGradesByClassNameAsync(className);
        IEnumerable<StudentGradeResponse> result = StudentGradeResponse.Map(items);
        return result;
    }

    public async Task<StudentGradeResponse> GetStudentGradeByClassNameAsync(string studentId, string className)
    {
        StudentGrade item = await _repository.GetStudentGradeByClassNameAsync(studentId, className);
        StudentGradeResponse result = StudentGradeResponse.Map(item);
        return result;
    }

    public async Task<StudentGradeAverageResponse> GetStudentAverageGradeAsync(string studentId)
    {
        IEnumerable<StudentGrade> items = await _repository.GetStudentGradesByIdAsync(studentId);
        IEnumerable<StudentGradeResponse> grades = StudentGradeResponse.Map(items);
        var average = grades.Select(c => c.Grade).Average();
        return new StudentGradeAverageResponse(studentId, average);
    }

    public async Task AddGradeToSpecificStudentAsync(string studentId, StudentGradeRequest request)
    {
        var studentGrade = new StudentGrade(studentId, request.ClassName, request.FirstName, request.LastName, request.Grade);
        await _repository.AddStudentGradeAsync(studentGrade);
    }

    public async Task AddStudentGradeAsync(StudentGradeRequest request)
    {
        var studentGrade = new StudentGrade(request.ClassName, request.FirstName, request.LastName, request.Grade);
        await _repository.AddStudentGradeAsync(studentGrade);
    }

    public async Task UpdateStudentGradeAsync(string studentId, StudentGradeUpdateRequest request)
    {
        var existingItem = await _repository.GetStudentGradeByClassNameAsync(studentId, request.ClassName);
        var studentGrade = StudentGrade.UpdateStudentGrade(studentId, existingItem, request.Grade);
        await _repository.UpdateStudentGradeAsync(studentGrade);
    }
}
