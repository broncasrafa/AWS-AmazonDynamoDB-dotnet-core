namespace AmazonDynamoDB.Core.DTO.Request;

public class StudentGradeUpdateRequest
{
    public string ClassName { get; set; }
    public double Grade { get; set; }
}
