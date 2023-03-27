namespace AmazonDynamoDB.Core.DTO.Request;

public class StudentGradeRequest
{
    public string ClassName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public double Grade { get; set; }
}