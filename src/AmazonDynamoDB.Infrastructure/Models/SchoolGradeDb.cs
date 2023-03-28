using Amazon.DynamoDBv2.DataModel;

namespace AmazonDynamoDB.Infrastructure.Models;


[DynamoDBTable("SchoolGrade")]
public class SchoolGradeDb
{
    [DynamoDBHashKey]
    public string StudentId { get; set; }

    [DynamoDBGlobalSecondaryIndexHashKey]
    public string ClassName { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public double Grade { get; set; }
    public string GradedAt { get; set; }
}
