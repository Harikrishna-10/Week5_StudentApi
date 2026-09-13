using StudentApi.Models;

namespace StudentApi.Repositories;

public class TeacherRepository : ITeacherRepository
{
    private readonly List<Teacher> _teachers = new()
    {
        new Teacher
        {
            Id = 1,
            Name = "Robert",
            Subject = "Mathematics",
            Email = "robert@example.com"
        },
        new Teacher
        {
            Id = 2,
            Name = "Sarah",
            Subject = "Science",
            Email = "sarah@example.com"
        }
    };

    public List<Teacher> GetAll()
    {
        return _teachers;
    }

    public Teacher? GetById(int id)
    {
        return _teachers.FirstOrDefault(t => t.Id == id);
    }

    public Teacher Add(Teacher teacher)
    {
        teacher.Id = _teachers.Count == 0
            ? 1
            : _teachers.Max(t => t.Id) + 1;

        _teachers.Add(teacher);

        return teacher;
    }

    public bool Update(int id, Teacher teacher)
    {
        var existingTeacher = GetById(id);

        if (existingTeacher == null)
        {
            return false;
        }

        existingTeacher.Name = teacher.Name;
        existingTeacher.Subject = teacher.Subject;
        existingTeacher.Email = teacher.Email;

        return true;
    }

    public bool Delete(int id)
    {
        var teacher = GetById(id);

        if (teacher == null)
        {
            return false;
        }

        _teachers.Remove(teacher);

        return true;
    }
}