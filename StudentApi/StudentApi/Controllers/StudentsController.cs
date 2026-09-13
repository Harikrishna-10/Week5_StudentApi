using Microsoft.AspNetCore.Mvc;
using StudentApi.DTOs;
using StudentApi.Models;
using StudentApi.Services;
using StudentApi.Strategies;

namespace StudentApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    private readonly IStudentService _studentService;
    private readonly PercentageGradeStrategy _percentageStrategy;
    private readonly GpaGradeStrategy _gpaStrategy;
    private readonly ILogger<StudentsController> _logger;

    public StudentsController(
        IStudentService studentService,
        PercentageGradeStrategy percentageStrategy,
        GpaGradeStrategy gpaStrategy,
        ILogger<StudentsController> logger)
    {
        _studentService = studentService;
        _percentageStrategy = percentageStrategy;
        _gpaStrategy = gpaStrategy;
        _logger = logger;
    }

    // GET: api/Students
    [HttpGet]
    public IActionResult GetAll()
    {
        var students = _studentService.GetAll();

        var studentDtos = students.Select(student => new StudentReadDto
        {
            Id = student.Id,
            Name = student.Name,
            Age = student.Age,
            Email = student.Email
        }).ToList();

        return Ok(studentDtos);
    }

    // GET: api/Students/1
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var student = _studentService.GetById(id);

        if (student == null)
        {
            // Step 15 - Log 404
            _logger.LogWarning(
                "Student not found. StudentId: {StudentId}",
                id);

            return NotFound();
        }

        var studentDto = new StudentReadDto
        {
            Id = student.Id,
            Name = student.Name,
            Age = student.Age,
            Email = student.Email
        };

        return Ok(studentDto);
    }

    // POST: api/Students
    [HttpPost]
    public IActionResult Create(StudentCreateDto studentDto)
    {
        var student = new Student
        {
            Name = studentDto.Name,
            Age = studentDto.Age,
            Email = studentDto.Email,
            InternalNotes = "Created through API"
        };

        var createdStudent = _studentService.Add(student);

        // Step 14 - Log when a student is created
        _logger.LogInformation(
            "Student created successfully. StudentId: {StudentId}, Name: {StudentName}",
            createdStudent.Id,
            createdStudent.Name);

        var responseDto = new StudentReadDto
        {
            Id = createdStudent.Id,
            Name = createdStudent.Name,
            Age = createdStudent.Age,
            Email = createdStudent.Email
        };

        return CreatedAtAction(
            nameof(GetById),
            new { id = responseDto.Id },
            responseDto);
    }

    // PUT: api/Students/1
    [HttpPut("{id}")]
    public IActionResult Update(int id, StudentCreateDto studentDto)
    {
        var student = new Student
        {
            Id = id,
            Name = studentDto.Name,
            Age = studentDto.Age,
            Email = studentDto.Email
        };

        var updated = _studentService.Update(id, student);

        if (!updated)
        {
            return NotFound();
        }

        return NoContent();
    }

    // DELETE: api/Students/1
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var deleted = _studentService.Delete(id);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }

    // GET: api/Students/grade?marks=85&strategy=gpa
    [HttpGet("grade")]
    public IActionResult CalculateGrade(
        int marks,
        string strategy = "percentage")
    {
        if (marks < 0 || marks > 100)
        {
            return BadRequest("Marks must be between 0 and 100.");
        }

        IGradeStrategy gradeStrategy;

        if (strategy.Equals("gpa", StringComparison.OrdinalIgnoreCase))
        {
            gradeStrategy = _gpaStrategy;
        }
        else
        {
            gradeStrategy = _percentageStrategy;
        }

        var grade = gradeStrategy.CalculateGrade(marks);

        return Ok(new GradeResponseDto
        {
            Marks = marks,
            Strategy = strategy,
            Grade = grade
        });
    }
    // GET: api/Students/search?name=John
    [HttpGet("search")]
    public IActionResult Search(string name)
    {
        var students = _studentService.SearchByName(name);

        var studentDtos = students.Select(student => new StudentReadDto
        {
            Id = student.Id,
            Name = student.Name,
            Age = student.Age,
            Email = student.Email
        }).ToList();

        return Ok(studentDtos);
    }
}