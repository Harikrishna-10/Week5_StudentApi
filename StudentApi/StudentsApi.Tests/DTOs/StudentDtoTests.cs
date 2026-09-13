using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StudentApi.Controllers;
using StudentApi.DTOs;
using StudentApi.Repositories;
using StudentApi.Services;
using StudentApi.Strategies;

namespace StudentApi.Tests.DTOs;

public class StudentDtoTests
{
    [Fact]
    public void GetById_ShouldNotExposeInternalNotes()
    {
        var repository = new InMemoryRepository<StudentApi.Models.Student>();
        var service = new StudentService(repository);

        var logger = LoggerFactory
            .Create(builder => { })
            .CreateLogger<StudentsController>();

        var controller = new StudentsController(
            service,
            new PercentageGradeStrategy(),
            new GpaGradeStrategy(),
            logger);

        var result = controller.GetById(1);

        var okResult = Assert.IsType<OkObjectResult>(result);

        var dto = Assert.IsType<StudentReadDto>(okResult.Value);

        Assert.Equal(1, dto.Id);
        Assert.Equal("John", dto.Name);

        // StudentReadDto does not contain InternalNotes.
        var internalNotesProperty =
            typeof(StudentReadDto).GetProperty("InternalNotes");

        Assert.Null(internalNotesProperty);
    }
}