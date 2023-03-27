namespace AmazonDynamoDB.Core.DTO.Response;

public class StudentGradeAverageResponse
{
    public string StudentId { get; private set; }
    public double? GradeAverage { get; private set; }

    public StudentGradeAverageResponse(string studentId, double? gradeAverage)
    {
        StudentId = studentId;
        GradeAverage = gradeAverage;
    }
}
