using AmazonDynamoDB.Core.Entities;
using AmazonDynamoDB.Core.Interfaces.Repositories;
using AmazonDynamoDB.Infrastructure.Mappers;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;

namespace AmazonDynamoDB.Infrastructure.Repositories;

public class StudentGradeRepository : IStudentGradeRepository
{
    private const string _TableName = "SchoolGrade";
    private readonly Table _Table;

    public StudentGradeRepository(IAmazonDynamoDB dynamoDbClient)
    {
        _Table = Table.LoadTable(dynamoDbClient, _TableName);
    }



    public async Task<IEnumerable<StudentGrade>> GetAllStudentsAsync()
    {
        var config = new ScanOperationConfig();
        List<Document> documents = await _Table.Scan(config).GetRemainingAsync();
        IEnumerable<StudentGrade> result = StudentGradeMapper.Map(documents);
        return result.DistinctBy(c => c.StudentId);
    }

    public async Task<IEnumerable<StudentGrade>> GetStudentGradesByIdAsync(string studentId)
    {
        QueryFilter filter = new QueryFilter("StudentId", QueryOperator.Equal, studentId);

        List<Document> document = await _Table.Query(filter).GetRemainingAsync();
        IEnumerable<StudentGrade> result = StudentGradeMapper.Map(document);
        return result;
    }

    public async Task<IEnumerable<StudentGrade>> GetStudentGradesByClassNameAsync(string studentId, string className)
    {
        QueryFilter filter = new QueryFilter("StudentId", QueryOperator.Equal, studentId);
        filter.AddCondition("ClassName", QueryOperator.BeginsWith, className);

        List<Document> document = await _Table.Query(filter).GetRemainingAsync();
        IEnumerable<StudentGrade> result = StudentGradeMapper.Map(document);
        return result;
    }

    public async Task<IEnumerable<StudentGrade>> GetGradesByClassNameAsync(string className)
    {
        QueryFilter filter = new QueryFilter("ClassName", QueryOperator.Equal, className);
        QueryOperationConfig operationConfig = new QueryOperationConfig
        {
            IndexName = "ClassName-index",
            Filter = filter
        };

        List<Document> document = await _Table.Query(operationConfig).GetRemainingAsync();
        IEnumerable<StudentGrade> result = StudentGradeMapper.Map(document);
        return result;
    }

    public async Task<StudentGrade> GetStudentGradeByClassNameAsync(string studentId, string className)
    {
        Document document = await _Table.GetItemAsync(studentId, className);
        StudentGrade result = StudentGradeMapper.Map(document);
        return result;
    }

    public async Task AddStudentGradeAsync(StudentGrade studentGrade)
    {
        Document document = StudentGradeMapper.MapToDocument(studentGrade);
        await _Table.PutItemAsync(document);
    }

    public async Task UpdateStudentGradeAsync(StudentGrade studentGrade)
    {
        Document document = StudentGradeMapper.MapToDocument(studentGrade);
        await _Table.UpdateItemAsync(document);
    }
}
