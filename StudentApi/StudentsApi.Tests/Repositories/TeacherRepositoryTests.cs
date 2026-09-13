using StudentApi.Models;
using StudentApi.Repositories;

namespace StudentApi.Tests.Repositories;

public class TeacherRepositoryTests
{
    private readonly TeacherRepository _repository;

    public TeacherRepositoryTests()
    {
        _repository = new TeacherRepository();
    }

    [Fact]
    public void GetAll_ShouldReturnTeachers()
    {
        // Act
        var result = _repository.GetAll();

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public void GetById_ShouldReturnTeacher_WhenTeacherExists()
    {
        // Arrange
        var teacher = _repository.Add(new Teacher
        {
            Name = "John",
            Email = "john@example.com"
        });

        // Act
        var result = _repository.GetById(teacher.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(teacher.Id, result.Id);
        Assert.Equal("John", result.Name);
    }

    [Fact]
    public void GetById_ShouldReturnNull_WhenTeacherDoesNotExist()
    {
        // Act
        var result = _repository.GetById(99999);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void Add_ShouldAddTeacher()
    {
        // Arrange
        var teacher = new Teacher
        {
            Name = "Alice",
            Email = "alice@example.com"
        };

        // Act
        var result = _repository.Add(teacher);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Id > 0);
        Assert.Equal("Alice", result.Name);
    }

    [Fact]
    public void Update_ShouldReturnTrue_WhenTeacherExists()
    {
        // Arrange
        var teacher = _repository.Add(new Teacher
        {
            Name = "John",
            Email = "john@example.com"
        });

        var updatedTeacher = new Teacher
        {
            Id = teacher.Id,
            Name = "Updated John",
            Email = "updated@example.com"
        };

        // Act
        var result = _repository.Update(
            teacher.Id,
            updatedTeacher);

        // Assert
        Assert.True(result);

        var storedTeacher = _repository.GetById(teacher.Id);

        Assert.NotNull(storedTeacher);
        Assert.Equal("Updated John", storedTeacher.Name);
    }

    [Fact]
    public void Update_ShouldReturnFalse_WhenTeacherDoesNotExist()
    {
        // Arrange
        var teacher = new Teacher
        {
            Id = 99999,
            Name = "Unknown",
            Email = "unknown@example.com"
        };

        // Act
        var result = _repository.Update(
            99999,
            teacher);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Delete_ShouldReturnTrue_WhenTeacherExists()
    {
        // Arrange
        var teacher = _repository.Add(new Teacher
        {
            Name = "John",
            Email = "john@example.com"
        });

        // Act
        var result = _repository.Delete(teacher.Id);

        // Assert
        Assert.True(result);
        Assert.Null(_repository.GetById(teacher.Id));
    }

    [Fact]
    public void Delete_ShouldReturnFalse_WhenTeacherDoesNotExist()
    {
        // Act
        var result = _repository.Delete(99999);

        // Assert
        Assert.False(result);
    }
}