using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StudentApi.Controllers;
using StudentApi.Repositories;
using StudentApi.Services;
using StudentApi.Strategies;

namespace StudentApi.Tests.Controllers;

public class StudentsControllerTests
{
    private readonly StudentsController _controller;

    public StudentsControllerTests()
    {
        var repository = new InMemoryRepository<StudentApi.Models.Student>();
        var service = new StudentService(repository);

        var percentageStrategy = new PercentageGradeStrategy();
        var gpaStrategy = new GpaGradeStrategy();

        var logger = LoggerFactory
            .Create(builder => { })
            .CreateLogger<StudentsController>();

        _controller = new StudentsController(
            service,
            percentageStrategy,
            gpaStrategy,
            logger);
    }

    [Fact]
    public void GetAll_ShouldReturn200()
    {
        var result = _controller.GetAll();

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public void GetById_ShouldReturn200_WhenStudentExists()
    {
        var result = _controller.GetById(1);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public void GetById_ShouldReturn404_WhenStudentDoesNotExist()
    {
        var result = _controller.GetById(999);

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public void Create_ShouldReturn201()
    {
        var dto = new StudentApi.DTOs.StudentCreateDto
        {
            Name = "Test Student",
            Age = 25,
            Email = "test@example.com"
        };

        var result = _controller.Create(dto);

        Assert.IsType<CreatedAtActionResult>(result);
    }

    [Fact]
    public void Update_ShouldReturn204_WhenStudentExists()
    {
        var dto = new StudentApi.DTOs.StudentCreateDto
        {
            Name = "Updated Student",
            Age = 26,
            Email = "updated@example.com"
        };

        var result = _controller.Update(1, dto);

        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public void Update_ShouldReturn404_WhenStudentDoesNotExist()
    {
        var dto = new StudentApi.DTOs.StudentCreateDto
        {
            Name = "Updated Student",
            Age = 26,
            Email = "updated@example.com"
        };

        var result = _controller.Update(999, dto);

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public void Delete_ShouldReturn204_WhenStudentExists()
    {
        var result = _controller.Delete(1);

        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public void Delete_ShouldReturn404_WhenStudentDoesNotExist()
    {
        var result = _controller.Delete(999);

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public void CalculateGrade_ShouldReturn400_WhenMarksAreInvalid()
    {
        var result = _controller.CalculateGrade(101);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public void CalculateGrade_ShouldReturn200_WhenMarksAreValid()
    {
        var result = _controller.CalculateGrade(85, "percentage");

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public void CalculateGrade_ShouldSelectGpaStrategy()
    {
        var result = _controller.CalculateGrade(85, "gpa");

        var okResult = Assert.IsType<OkObjectResult>(result);

        var response = Assert.IsType<StudentApi.DTOs.GradeResponseDto>(
            okResult.Value);

        Assert.Equal("gpa", response.Strategy);
        Assert.Equal("3.5", response.Grade);
    }
}