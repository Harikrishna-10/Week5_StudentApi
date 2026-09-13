namespace StudentApi.Strategies;

public class PercentageGradeStrategy : IGradeStrategy
{
    public string CalculateGrade(int marks)
    {
        return $"{marks}%";
    }
}