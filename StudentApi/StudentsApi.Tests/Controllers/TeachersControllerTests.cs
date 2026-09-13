using Microsoft.AspNetCore.Mvc;
using Moq;
using StudentApi.Controllers;
using StudentApi.DTOs;
using StudentApi.Models;
using StudentApi.Services;

namespace StudentApi.Tests.Controllers;

public class TeachersControllerTests
{
    private readonly Mock<ITeacherService> _serviceMock;
    private readonly TeachersController _controller;

    public TeachersControllerTests()
    {
        _serviceMock = new Mock<ITeacherService>();
        _controller = new TeachersController(
            _serviceMock.Object);
    }

    [Fact]
    public void GetAll_ShouldReturnOk()
    {
        // Arrange
        var teachers = new List<Teacher>
        {
            new Teacher
            {
                Id = 1,
                Name = "John",
                Email = "john@example.com"
            },
            new Teacher
            {
                Id = 2,
                Name = "Alice",
                Email = "alice@example.com"
            }
        };

        _serviceMock
            .Setup(s => s.GetAll())
            .Returns(teachers);

        // Act
        var result = _controller.GetAll();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);

        var response =
            Assert.IsAssignableFrom<IEnumerable<TeacherReadDto>>(
                okResult.Value);

        Assert.Equal(2, response.Count());
    }

    [Fact]
    public void GetById_ShouldReturnOk_WhenTeacherExists()
    {
        // Arrange
        var teacher = new Teacher
        {
            Id = 1,
            Name = "John",
            Email = "john@example.com"
        };

        _serviceMock
            .Setup(s => s.GetById(1))
            .Returns(teacher);

        // Act
        var result = _controller.GetById(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);

        var response =
            Assert.IsType<TeacherReadDto>(okResult.Value);

        Assert.Equal(1, response.Id);
        Assert.Equal("John", response.Name);
        Assert.Equal("john@example.com", response.Email);
    }

    [Fact]
    public void GetById_ShouldReturnNotFound_WhenTeacherDoesNotExist()
    {
        // Arrange
        _serviceMock
            .Setup(s => s.GetById(999))
            .Returns((Teacher?)null);

        // Act
        var result = _controller.GetById(999);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public void Create_ShouldReturnCreated()
    {
        // Arrange
        var dto = new TeacherCreateDto
        {
            Name = "John",
            Email = "john@example.com"
        };

        var createdTeacher = new Teacher
        {
            Id = 1,
            Name = "John",
            Email = "john@example.com"
        };

        _serviceMock
            .Setup(s => s.Add(It.IsAny<Teacher>()))
            .Returns(createdTeacher);

        // Act
        var result = _controller.Create(dto);

        // Assert
        var createdResult =
            Assert.IsType<CreatedAtActionResult>(result);

        Assert.Equal(nameof(_controller.GetById),
            createdResult.ActionName);

        var response =
            Assert.IsType<TeacherReadDto>(
                createdResult.Value);

        Assert.Equal(1, response.Id);
        Assert.Equal("John", response.Name);
    }

    [Fact]
    public void Update_ShouldReturnNoContent_WhenTeacherExists()
    {
        // Arrange
        var dto = new TeacherCreateDto
        {
            Name = "Updated John",
            Email = "updated@example.com"
        };

        _serviceMock
            .Setup(s => s.Update(
                1,
                It.IsAny<Teacher>()))
            .Returns(true);

        // Act
        var result = _controller.Update(1, dto);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public void Update_ShouldReturnNotFound_WhenTeacherDoesNotExist()
    {
        // Arrange
        var dto = new TeacherCreateDto
        {
            Name = "Unknown",
            Email = "unknown@example.com"
        };

        _serviceMock
            .Setup(s => s.Update(
                999,
                It.IsAny<Teacher>()))
            .Returns(false);

        // Act
        var result = _controller.Update(999, dto);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public void Delete_ShouldReturnNoContent_WhenTeacherExists()
    {
        // Arrange
        _serviceMock
            .Setup(s => s.Delete(1))
            .Returns(true);

        // Act
        var result = _controller.Delete(1);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public void Delete_ShouldReturnNotFound_WhenTeacherDoesNotExist()
    {
        // Arrange
        _serviceMock
            .Setup(s => s.Delete(999))
            .Returns(false);

        // Act
        var result = _controller.Delete(999);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }
}