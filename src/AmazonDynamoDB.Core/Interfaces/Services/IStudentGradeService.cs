using AmazonDynamoDB.Core.DTO.Request;
using AmazonDynamoDB.Core.DTO.Response;

namespace AmazonDynamoDB.Core.Interfaces.Services;

public interface IStudentGradeService
{
    Task<IEnumerable<StudentResponse>> GetAllStudentsAsync();
    Task<IEnumerable<StudentGradeResponse>> GetStudentGradesByIdAsync(string studentId);
    Task<IEnumerable<StudentGradeResponse>> GetStudentGradesByClassNameAsync(string studentId, string className);
    Task<IEnumerable<StudentGradeResponse>> GetGradesByClassNameAsync(string className);
    Task<StudentGradeResponse> GetStudentGradeByClassNameAsync(string studentId, string className);
    Task AddGradeToSpecificStudentAsync(string studentId, StudentGradeRequest request);
    Task AddStudentGradeAsync(StudentGradeRequest studentGrade);
    Task UpdateStudentGradeAsync(string studentId, StudentGradeUpdateRequest request);
    Task<StudentGradeAverageResponse> GetStudentAverageGradeAsync(string studentId);
}
