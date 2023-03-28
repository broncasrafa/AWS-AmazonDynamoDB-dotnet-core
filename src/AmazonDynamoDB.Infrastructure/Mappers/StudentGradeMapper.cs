using Amazon.DynamoDBv2.DocumentModel;
using AmazonDynamoDB.Core.Entities;

namespace AmazonDynamoDB.Infrastructure.Mappers;

public static class StudentGradeMapper
{
    public static IEnumerable<StudentGrade> Map(IEnumerable<Document> items)
    {
        if (items == null)
            return Enumerable.Empty<StudentGrade>();

        return items.Select(item => Map(item));
    }
    public static StudentGrade Map(Document item)
    {
        if (item == null) return null;
        return new StudentGrade(
            studentId: item["StudentId"],
            className: item["ClassName"],
            firstName: item["FirstName"],
            lastName: item["LastName"],
            grade: item["Grade"] == null ? null : Convert.ToDouble(item["Grade"].ToString().Replace(".", ",")),
            gradedAt: item["GradedAt"]);
    }

    public static Document MapToDocument(StudentGrade obj)
    {
        return new Document
        {
            ["StudentId"] = obj.StudentId,
            ["ClassName"] = obj.ClassName,
            ["FirstName"] = obj.FirstName,
            ["LastName"] = obj.LastName,
            ["Grade"] = obj.Grade,
            ["GradedAt"] = obj.GradedAt
        };
    }
}