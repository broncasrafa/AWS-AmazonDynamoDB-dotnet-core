using AmazonDynamoDB.Core.Entities;

namespace AmazonDynamoDB.Core.DTO.Response;

public class StudentResponse
{
    public string StudentId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Name { get; set; }

    internal static IEnumerable<StudentResponse> Map(IEnumerable<StudentGrade> items)
    {
        if (items == null)
            return Enumerable.Empty<StudentResponse>();

        return items.Select(x => Map(x));
    }

    internal static StudentResponse Map(StudentGrade item)
    {
        if (item == null) return null;

        return new StudentResponse
        {
            StudentId = item.StudentId,
            FirstName = item.FirstName,
            LastName = item.LastName,
            Name = $"{item.FirstName} {item.LastName}"
        };
    }
}
