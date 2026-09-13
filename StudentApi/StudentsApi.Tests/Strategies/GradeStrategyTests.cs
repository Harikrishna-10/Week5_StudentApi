using StudentApi.Strategies;

namespace StudentApi.Tests.Strategies;

public class GradeStrategyTests
{
    [Fact]
    public void PercentageStrategy_ShouldReturnPercentage()
    {
        var strategy = new PercentageGradeStrategy();

        var result = strategy.CalculateGrade(85);

        Assert.Equal("85%", result);
    }

    [Fact]
    public void PercentageStrategy_ShouldReturnZeroPercentage()
    {
        var strategy = new PercentageGradeStrategy();

        var result = strategy.CalculateGrade(0);

        Assert.Equal("0%", result);
    }

    [Fact]
    public void GpaStrategy_ShouldReturnFour_WhenMarksAre90OrAbove()
    {
        var strategy = new GpaGradeStrategy();

        var result = strategy.CalculateGrade(95);

        Assert.Equal("4.0", result);
    }

    [Fact]
    public void GpaStrategy_ShouldReturnThreePointFive_WhenMarksAre80To89()
    {
        var strategy = new GpaGradeStrategy();

        var result = strategy.CalculateGrade(85);

        Assert.Equal("3.5", result);
    }

    [Fact]
    public void GpaStrategy_ShouldReturnThree_WhenMarksAre70To79()
    {
        var strategy = new GpaGradeStrategy();

        var result = strategy.CalculateGrade(75);

        Assert.Equal("3.0", result);
    }

    [Fact]
    public void GpaStrategy_ShouldReturnTwoPointFive_WhenMarksAre60To69()
    {
        var strategy = new GpaGradeStrategy();

        var result = strategy.CalculateGrade(65);

        Assert.Equal("2.5", result);
    }

    [Fact]
    public void GpaStrategy_ShouldReturnTwo_WhenMarksAre50To59()
    {
        var strategy = new GpaGradeStrategy();

        var result = strategy.CalculateGrade(55);

        Assert.Equal("2.0", result);
    }

    [Fact]
    public void GpaStrategy_ShouldReturnZero_WhenMarksAreBelow50()
    {
        var strategy = new GpaGradeStrategy();

        var result = strategy.CalculateGrade(40);

        Assert.Equal("0.0", result);
    }
}