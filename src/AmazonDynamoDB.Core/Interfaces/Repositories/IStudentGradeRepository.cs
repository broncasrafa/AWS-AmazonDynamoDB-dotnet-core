using AmazonDynamoDB.Core.Entities;

namespace AmazonDynamoDB.Core.Interfaces.Repositories;
public interface IStudentGradeRepository
{
    Task<IEnumerable<StudentGrade>> GetAllStudentsAsync();
    Task<IEnumerable<StudentGrade>> GetStudentGradesByIdAsync(string studentId);
    Task<IEnumerable<StudentGrade>> GetStudentGradesByClassNameAsync(string studentId, string className);
    Task<IEnumerable<StudentGrade>> GetGradesByClassNameAsync(string className);
    Task<StudentGrade> GetStudentGradeByClassNameAsync(string studentId, string className);
    Task AddStudentGradeAsync(StudentGrade studentGrade);
    Task UpdateStudentGradeAsync(StudentGrade studentGrade);
}
