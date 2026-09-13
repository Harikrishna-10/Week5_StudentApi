using Microsoft.AspNetCore.Mvc;
using StudentApi.DTOs;
using StudentApi.Models;
using StudentApi.Services;

namespace StudentApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TeachersController : ControllerBase
{
    private readonly ITeacherService _teacherService;

    public TeachersController(ITeacherService teacherService)
    {
        _teacherService = teacherService;
    }

    // GET: api/Teachers
    [HttpGet]
    public IActionResult GetAll()
    {
        var teachers = _teacherService.GetAll();

        var teacherDtos = teachers.Select(teacher => new TeacherReadDto
        {
            Id = teacher.Id,
            Name = teacher.Name,
            Subject = teacher.Subject,
            Email = teacher.Email
        }).ToList();

        return Ok(teacherDtos);
    }

    // GET: api/Teachers/1
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var teacher = _teacherService.GetById(id);

        if (teacher == null)
        {
            return NotFound();
        }

        var teacherDto = new TeacherReadDto
        {
            Id = teacher.Id,
            Name = teacher.Name,
            Subject = teacher.Subject,
            Email = teacher.Email
        };

        return Ok(teacherDto);
    }

    // POST: api/Teachers
    [HttpPost]
    public IActionResult Create(TeacherCreateDto teacherDto)
    {
        var teacher = new Teacher
        {
            Name = teacherDto.Name,
            Subject = teacherDto.Subject,
            Email = teacherDto.Email
        };

        var createdTeacher = _teacherService.Add(teacher);

        var responseDto = new TeacherReadDto
        {
            Id = createdTeacher.Id,
            Name = createdTeacher.Name,
            Subject = createdTeacher.Subject,
            Email = createdTeacher.Email
        };

        return CreatedAtAction(
            nameof(GetById),
            new { id = responseDto.Id },
            responseDto);
    }

    // PUT: api/Teachers/1
    [HttpPut("{id}")]
    public IActionResult Update(
        int id,
        TeacherCreateDto teacherDto)
    {
        var teacher = new Teacher
        {
            Id = id,
            Name = teacherDto.Name,
            Subject = teacherDto.Subject,
            Email = teacherDto.Email
        };

        var updated = _teacherService.Update(id, teacher);

        if (!updated)
        {
            return NotFound();
        }

        return NoContent();
    }

    // DELETE: api/Teachers/1
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var deleted = _teacherService.Delete(id);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}