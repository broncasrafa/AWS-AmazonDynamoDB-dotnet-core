using AmazonDynamoDB.Core.Entities;

namespace AmazonDynamoDB.Core.DTO.Response;

public class StudentGradeResponse
{
    public string StudentId { get; set; }
    public string ClassName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public double? Grade { get; set; }
    public string GradedAt { get; set; }
    public string Name { get; set; }

    internal static IEnumerable<StudentGradeResponse> Map(IEnumerable<StudentGrade> items)
    {
        if (items == null)
            return Enumerable.Empty<StudentGradeResponse>();

        return items.Select(x => Map(x));
    }

    internal static StudentGradeResponse Map(StudentGrade item)
    {
        if (item == null) return null;

        return new StudentGradeResponse
        {
            StudentId = item.StudentId,
            ClassName = item.ClassName,
            FirstName = item.FirstName,
            LastName = item.LastName,
            Grade = item.Grade,
            GradedAt = item.GradedAt,
            Name = $"{item.FirstName} {item.LastName}"
        };
    }
}
