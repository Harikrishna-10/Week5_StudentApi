namespace StudentApi.Strategies;

public class GpaGradeStrategy : IGradeStrategy
{
    public string CalculateGrade(int marks)
    {
        var gpa = marks switch
        {
            >= 90 => 4.0,
            >= 80 => 3.5,
            >= 70 => 3.0,
            >= 60 => 2.5,
            >= 50 => 2.0,
            _ => 0.0
        };

        return gpa.ToString("0.0");
    }
}