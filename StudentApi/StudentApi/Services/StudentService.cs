using StudentApi.Models;
using StudentApi.Repositories;

namespace StudentApi.Services;

public class StudentService : IStudentService
{
    private readonly IRepository<Student> _repository;

    public StudentService(IRepository<Student> repository)
    {
        _repository = repository;
    }

    public List<Student> GetAll()
    {
        return _repository.GetAll();
    }

    public Student? GetById(int id)
    {
        return _repository.GetById(id);
    }

    public Student Add(Student student)
    {
        return _repository.Add(student);
    }

    public bool Update(int id, Student student)
    {
        return _repository.Update(id, student);
    }

    public bool Delete(int id)
    {
        return _repository.Delete(id);
    }

    public List<Student> SearchByName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return new List<Student>();
        }

        var students = _repository.GetAll();

        return students
            .Where(student =>
                student.Name.Contains(
                    name,
                    StringComparison.OrdinalIgnoreCase))
            .ToList();
    }
}