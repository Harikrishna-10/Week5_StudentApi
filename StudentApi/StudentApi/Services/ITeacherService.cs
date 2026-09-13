using StudentApi.Models;

namespace StudentApi.Services;

public interface ITeacherService
{
	List<Teacher> GetAll();

	Teacher? GetById(int id);

	Teacher Add(Teacher teacher);

	bool Update(int id, Teacher teacher);

	bool Delete(int id);
}