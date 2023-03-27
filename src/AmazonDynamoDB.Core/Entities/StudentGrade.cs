namespace AmazonDynamoDB.Core.Entities;

public class StudentGrade
{
    public string StudentId { get; private set; }
    public string ClassName { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public double? Grade { get; private set; }
    public string GradedAt { get; private set; }


    public StudentGrade(string className, string firstName, string lastName, double? grade)
    {
        StudentId = Guid.NewGuid().ToString();
        ClassName = className;
        FirstName = firstName;
        LastName = lastName;
        Grade = grade;
        GradedAt = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
    }

    public StudentGrade(string studentId, string className, string firstName, string lastName, double? grade)
    {
        StudentId = studentId;
        ClassName = className;
        FirstName = firstName;
        LastName = lastName;
        Grade = grade;
        GradedAt = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
    }

    public StudentGrade(string studentId, string className, string firstName, string lastName, double? grade, string gradedAt)
    {
        StudentId = studentId;
        ClassName = className;
        FirstName = firstName;
        LastName = lastName;
        Grade = grade;
        GradedAt = gradedAt;
    }

    public static StudentGrade UpdateStudentGrade(string studentId, StudentGrade existingItem, double grade)
    {
        return new StudentGrade(studentId, existingItem.ClassName, existingItem.FirstName, existingItem.LastName, grade);
    }
}
