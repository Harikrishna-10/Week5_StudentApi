using StudentApi.Models;

namespace StudentApi.Services;

public interface IStudentService
{
    List<Student> GetAll();

    Student? GetById(int id);

    Student Add(Student student);

    bool Update(int id, Student student);

    bool Delete(int id);

    List<Student> SearchByName(string name);
}