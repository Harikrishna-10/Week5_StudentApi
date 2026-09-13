using Moq;
using StudentApi.Models;
using StudentApi.Repositories;
using StudentApi.Services;

namespace StudentApi.Tests.Services;

public class StudentServiceTests
{
    private readonly Mock<IRepository<Student>> _repositoryMock;
    private readonly StudentService _service;

    public StudentServiceTests()
    {
        _repositoryMock = new Mock<IRepository<Student>>();
        _service = new StudentService(_repositoryMock.Object);
    }

    [Fact]
    public void GetAll_ShouldReturnAllStudents()
    {
        var students = new List<Student>
        {
            new Student
            {
                Id = 1,
                Name = "John",
                Age = 20,
                Email = "john@example.com"
            },
            new Student
            {
                Id = 2,
                Name = "Alice",
                Age = 22,
                Email = "alice@example.com"
            }
        };

        _repositoryMock
            .Setup(r => r.GetAll())
            .Returns(students);

        var result = _service.GetAll();

        Assert.Equal(2, result.Count);
    }

    [Fact]
    public void GetById_ShouldReturnStudent_WhenStudentExists()
    {
        var student = new Student
        {
            Id = 1,
            Name = "John",
            Age = 20,
            Email = "john@example.com"
        };

        _repositoryMock
            .Setup(r => r.GetById(1))
            .Returns(student);

        var result = _service.GetById(1);

        Assert.NotNull(result);
        Assert.Equal("John", result.Name);
    }

    [Fact]
    public void GetById_ShouldReturnNull_WhenStudentDoesNotExist()
    {
        _repositoryMock
            .Setup(r => r.GetById(999))
            .Returns((Student?)null);

        var result = _service.GetById(999);

        Assert.Null(result);
    }

    [Fact]
    public void Add_ShouldReturnAddedStudent()
    {
        var student = new Student
        {
            Name = "David",
            Age = 25,
            Email = "david@example.com"
        };

        _repositoryMock
            .Setup(r => r.Add(student))
            .Returns(student);

        var result = _service.Add(student);

        Assert.Same(student, result);
        _repositoryMock.Verify(r => r.Add(student), Times.Once);
    }

    [Fact]
    public void Update_ShouldReturnTrue_WhenRepositoryUpdatesStudent()
    {
        var student = new Student
        {
            Id = 1,
            Name = "Updated",
            Age = 25,
            Email = "updated@example.com"
        };

        _repositoryMock
            .Setup(r => r.Update(1, student))
            .Returns(true);

        var result = _service.Update(1, student);

        Assert.True(result);
    }

    [Fact]
    public void Update_ShouldReturnFalse_WhenStudentDoesNotExist()
    {
        var student = new Student
        {
            Id = 999,
            Name = "Test",
            Age = 25,
            Email = "test@example.com"
        };

        _repositoryMock
            .Setup(r => r.Update(999, student))
            .Returns(false);

        var result = _service.Update(999, student);

        Assert.False(result);
    }

    [Fact]
    public void Delete_ShouldReturnTrue_WhenStudentExists()
    {
        _repositoryMock
            .Setup(r => r.Delete(1))
            .Returns(true);

        var result = _service.Delete(1);

        Assert.True(result);
    }

    [Fact]
    public void Delete_ShouldReturnFalse_WhenStudentDoesNotExist()
    {
        _repositoryMock
            .Setup(r => r.Delete(999))
            .Returns(false);

        var result = _service.Delete(999);

        Assert.False(result);
    }

    [Fact]
    public void SearchByName_ShouldReturnMatchingStudents_CaseInsensitive()
    {
        var students = new List<Student>
        {
            new Student
            {
                Id = 1,
                Name = "John",
                Age = 20,
                Email = "john@example.com"
            },
            new Student
            {
                Id = 2,
                Name = "Alice",
                Age = 22,
                Email = "alice@example.com"
            }
        };

        _repositoryMock
            .Setup(r => r.GetAll())
            .Returns(students);

        var result = _service.SearchByName("john");

        Assert.Single(result);
        Assert.Equal("John", result[0].Name);
    }

    [Fact]
    public void SearchByName_ShouldReturnEmptyList_WhenNoMatch()
    {
        var students = new List<Student>
        {
            new Student
            {
                Id = 1,
                Name = "John",
                Age = 20,
                Email = "john@example.com"
            }
        };

        _repositoryMock
            .Setup(r => r.GetAll())
            .Returns(students);

        var result = _service.SearchByName("xyz");

        Assert.Empty(result);
    }

    [Fact]
    public void SearchByName_ShouldReturnEmptyList_WhenSearchIsEmpty()
    {
        // Arrange
        var repository = new Mock<IRepository<Student>>();

        repository
            .Setup(r => r.GetAll())
            .Returns(new List<Student>
            {
            new Student
            {
                Id = 1,
                Name = "John",
                Age = 20,
                Email = "john@example.com"
            }
            });

        var service = new StudentService(repository.Object);

        // Act
        var result = service.SearchByName("");

        // Assert
        Assert.Empty(result);
    }
}