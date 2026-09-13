using StudentApi.Models;

namespace StudentApi.Repositories;

public class InMemoryRepository<T> : IRepository<T>
{
    private readonly List<T> _items = new();

    public InMemoryRepository()
    {
        if (typeof(T) == typeof(Student))
        {
            _items.Add((T)(object)new Student
            {
                Id = 1,
                Name = "John",
                Age = 20,
                Email = "john@example.com"
            });

            _items.Add((T)(object)new Student
            {
                Id = 2,
                Name = "Alice",
                Age = 22,
                Email = "alice@example.com"
            });
        }
    }

    public List<T> GetAll()
    {
        return _items;
    }

    public T? GetById(int id)
    {
        foreach (var item in _items)
        {
            if (item is Student student && student.Id == id)
            {
                return (T)(object)student;
            }
        }

        return default;
    }

    public T Add(T entity)
    {
        if (entity is Student student)
        {
            student.Id = _items.Count + 1;
        }

        _items.Add(entity);

        return entity;
    }

    public bool Update(int id, T entity)
    {
        var existingEntity = GetById(id);

        if (existingEntity == null)
        {
            return false;
        }

        var index = _items.IndexOf(existingEntity);

        if (index == -1)
        {
            return false;
        }

        _items[index] = entity;

        return true;
    }

    public bool Delete(int id)
    {
        var entity = GetById(id);

        if (entity == null)
        {
            return false;
        }

        _items.Remove(entity);

        return true;
    }
}