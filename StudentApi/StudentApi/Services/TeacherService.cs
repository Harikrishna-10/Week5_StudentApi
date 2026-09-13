using StudentApi.Models;
using StudentApi.Repositories;

namespace StudentApi.Services;

public class TeacherService : ITeacherService
{
	private readonly ITeacherRepository _repository;

	public TeacherService(ITeacherRepository repository)
	{
		_repository = repository;
	}

	public List<Teacher> GetAll()
	{
		return _repository.GetAll();
	}

	public Teacher? GetById(int id)
	{
		return _repository.GetById(id);
	}

	public Teacher Add(Teacher teacher)
	{
		return _repository.Add(teacher);
	}

	public bool Update(int id, Teacher teacher)
	{
		return _repository.Update(id, teacher);
	}

	public bool Delete(int id)
	{
		return _repository.Delete(id);
	}
}