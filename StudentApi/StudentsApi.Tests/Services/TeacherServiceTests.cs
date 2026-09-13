using Moq;
using StudentApi.Models;
using StudentApi.Repositories;
using StudentApi.Services;

namespace StudentApi.Tests.Services;

public class TeacherServiceTests
{
    private readonly Mock<ITeacherRepository> _repositoryMock;
    private readonly TeacherService _service;

    public TeacherServiceTests()
    {
        _repositoryMock = new Mock<ITeacherRepository>();
        _service = new TeacherService(_repositoryMock.Object);
    }

    [Fact]
    public void GetAll_ShouldReturnAllTeachers()
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

        _repositoryMock
            .Setup(r => r.GetAll())
            .Returns(teachers);

        // Act
        var result = _service.GetAll();

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Equal("John", result[0].Name);
        Assert.Equal("Alice", result[1].Name);
    }

    [Fact]
    public void GetById_ShouldReturnTeacher_WhenTeacherExists()
    {
        // Arrange
        var teacher = new Teacher
        {
            Id = 1,
            Name = "John",
            Email = "john@example.com"
        };

        _repositoryMock
            .Setup(r => r.GetById(1))
            .Returns(teacher);

        // Act
        var result = _service.GetById(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("John", result.Name);
    }

    [Fact]
    public void GetById_ShouldReturnNull_WhenTeacherDoesNotExist()
    {
        // Arrange
        _repositoryMock
            .Setup(r => r.GetById(999))
            .Returns((Teacher?)null);

        // Act
        var result = _service.GetById(999);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void Add_ShouldReturnCreatedTeacher()
    {
        // Arrange
        var teacher = new Teacher
        {
            Id = 1,
            Name = "John",
            Email = "john@example.com"
        };

        _repositoryMock
            .Setup(r => r.Add(teacher))
            .Returns(teacher);

        // Act
        var result = _service.Add(teacher);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("John", result.Name);

        _repositoryMock.Verify(
            r => r.Add(teacher),
            Times.Once);
    }

    [Fact]
    public void Update_ShouldReturnTrue_WhenTeacherExists()
    {
        // Arrange
        var teacher = new Teacher
        {
            Id = 1,
            Name = "Updated John",
            Email = "updated@example.com"
        };

        _repositoryMock
            .Setup(r => r.Update(1, teacher))
            .Returns(true);

        // Act
        var result = _service.Update(1, teacher);

        // Assert
        Assert.True(result);

        _repositoryMock.Verify(
            r => r.Update(1, teacher),
            Times.Once);
    }

    [Fact]
    public void Update_ShouldReturnFalse_WhenTeacherDoesNotExist()
    {
        // Arrange
        var teacher = new Teacher
        {
            Id = 999,
            Name = "Unknown",
            Email = "unknown@example.com"
        };

        _repositoryMock
            .Setup(r => r.Update(999, teacher))
            .Returns(false);

        // Act
        var result = _service.Update(999, teacher);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Delete_ShouldReturnTrue_WhenTeacherExists()
    {
        // Arrange
        _repositoryMock
            .Setup(r => r.Delete(1))
            .Returns(true);

        // Act
        var result = _service.Delete(1);

        // Assert
        Assert.True(result);

        _repositoryMock.Verify(
            r => r.Delete(1),
            Times.Once);
    }

    [Fact]
    public void Delete_ShouldReturnFalse_WhenTeacherDoesNotExist()
    {
        // Arrange
        _repositoryMock
            .Setup(r => r.Delete(999))
            .Returns(false);

        // Act
        var result = _service.Delete(999);

        // Assert
        Assert.False(result);
    }
}