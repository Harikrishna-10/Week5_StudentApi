namespace StudentApi.Repositories;

public interface IRepository<T>
{
    List<T> GetAll();

    T? GetById(int id);

    T Add(T entity);

    bool Update(int id, T entity);

    bool Delete(int id);
}